using AutoMapper;
using AutoMapper.QueryableExtensions;
using OptiFabricMVC.Application.Interfaces;
using OptiFabricMVC.Application.ViewModels.JobVM;
using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.Services;

public class JobService : IJobService
{
    private readonly IJobRepository _jobRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public JobService(IJobRepository jobRepository, IMapper iMapper, IEmployeeRepository employeeRepository)
    {
        _jobRepository = jobRepository;
        _mapper = iMapper;
        _employeeRepository = employeeRepository;
    }

    public ListJobsVM GetAllJobs(int pageSize, int pageNo, string searchString)
    {
        var jobsQuery = _jobRepository.GetAllJobsFromDB();
        if (!string.IsNullOrWhiteSpace(searchString) && DateTime.TryParse(searchString, out DateTime parsedDate))
        {
            jobsQuery = jobsQuery.Where(m => m.CreatedAt.Date == parsedDate.Date);
        }

        var jobs = jobsQuery
            .ProjectTo<JobsForListVM>(_mapper.ConfigurationProvider)
            .ToList();
        

        var jobsList = new ListJobsVM()
        {
            PageSize = pageSize,
            CurrentPage = pageNo,
            SearchString = searchString,
            JobsForList = jobs,
            Count = jobs.Count
        };
        return jobsList;
    }

    public int AddJob(AddNewJobVM model)
    {
        var job = _mapper.Map<Job>(model);
        return _jobRepository.AddJobToDB(job);
    }

    public void StartJobEmployee(DateTime data, string? userId, int id)
    {
        var job = new JobEmployee()
        {
            CompletedQuantity = 0,
            EmployeeComments = "",
            MissingQuantity = 0,
            StartTime = data,
            CurrentWorkerId = userId,
            JobId = id
        };

        _jobRepository.StartJobEmployee(job);
    }

    public void StopJobEmployee(EndJobEmployeeVM model, DateTime data, string? userId, int id)
    {
        var endJob = _mapper.Map<JobEmployee>(model);
        _jobRepository.StopJobEmployee(endJob, id, userId);
    }

    public AddNewJobVM GetSelectedJob(int id)
    {
        var job = _jobRepository.GetSelectedJobFromDB(id);
        var jobVM = _mapper.Map<AddNewJobVM>(job);
        return jobVM;
    }

    public void EditJob(AddNewJobVM model)
    {
        var job = _mapper.Map<Job>(model);
        _jobRepository.EditJobDB(job);
    }

    public List<JobEmployee> GetAllJobsEmployee()
    {
        var jel = _jobRepository.GetAllJobsEmployeeFromDB();
        return _mapper.Map<List<JobEmployee>>(jel);
    }

    public List<DetailsJobEmployeeVM> GetAllJobsEmployeeDetails(int id)
    {
        var jel = _jobRepository.GetAllJobsEmployeeByIdFromDB(id).ToList();
        var jobEmployeeList = _mapper.Map<List<DetailsJobEmployeeVM>>(jel);
        foreach (var jobEmployee in jobEmployeeList)
        {
            var user = _employeeRepository.GetEmployee(jobEmployee.CurrentWorkerId);
            if (user != null)
            {
                jobEmployee.EmployeeName = user.Name;
                jobEmployee.EmployeeSurname = user.Surname;
            }
        }

        return jobEmployeeList;
    }

    public DetailsJobEmployeeVM GetJobEmployeeDetailById(int id)
    {
        var jobEmployee = _jobRepository.GetJobEmployeeFromDB(id);
        var jobEmployeeVM = _mapper.Map<DetailsJobEmployeeVM>(jobEmployee);
        return jobEmployeeVM;
    }

    public void EditJobEmployee(DetailsJobEmployeeVM model)
    {
        var jobEmployee = _mapper.Map<JobEmployee>(model);
        _jobRepository.EditJobEmployee(jobEmployee);
    }

    public void StartJobEmployee2(DateTime data, string? userId, int id,int selectedMachineId)
    {
        if (_jobRepository.IsMachineBusy(selectedMachineId))
        {
            throw new InvalidOperationException("Maszyna jest już zajęta");
        }
        
        var job = new JobEmployee()
        {
            CompletedQuantity = 0,
            EmployeeComments = "",
            MissingQuantity = 0,
            StartTime = data,
            CurrentWorkerId = userId,
            JobId = id,
            MachineId = selectedMachineId
        };
        

        _jobRepository.StartJobEmployee(job);
    }
}