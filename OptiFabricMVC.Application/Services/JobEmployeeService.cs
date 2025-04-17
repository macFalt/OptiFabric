using AutoMapper;
using OptiFabricMVC.Application.Interfaces;
using OptiFabricMVC.Application.ViewModels.JobEmployeeVM;
using OptiFabricMVC.Application.ViewModels.JobVM;
using OptiFabricMVC.Application.ViewModels.OperationVM;
using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.Services;

public class JobEmployeeService : IJobEmployeeService
{
    private readonly IMapper _mapper;
    private readonly IJobEmployeeRepository _jobEmployeeRepository;
    private readonly IJobRepository _jobRepository;
    private readonly IOperationRepository _operationRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMachinesRepository _machinesRepository;


    public JobEmployeeService(IMapper mapper, IJobEmployeeRepository jobEmployeeRepository,
        IJobRepository jobRepository, IOperationRepository operationRepository,
        IEmployeeRepository employeeRepository, IMachinesRepository machinesRepository)
    {
        _mapper = mapper;
        _jobEmployeeRepository = jobEmployeeRepository;
        _jobRepository = jobRepository;
        _operationRepository = operationRepository;
        _employeeRepository = employeeRepository;
        _machinesRepository = machinesRepository;
    }


    public async Task StartJobEmployee2(DateTime data, string? userId, int id, int selectedMachineId, int jobId)
    {
        if (_machinesRepository.IsMachineBusy(selectedMachineId))
        {
            throw new InvalidOperationException("Maszyna jest już zajęta");
        }

        if (_machinesRepository.IsMachineBroken(selectedMachineId))
        {
            throw new InvalidOperationException("Awaria maszyny");
        }

        var job = new JobEmployee()
        {
            CompletedQuantity = 0,
            EmployeeComments = "",
            MissingQuantity = 0,
            StartTime = data,
            CurrentWorkerId = userId,
            OperationId = id,
            MachineId = selectedMachineId,
            JobId = jobId
        };


        await _jobEmployeeRepository.StartJobEmployee(job);
    }

    public async Task StopJobEmployee(EndJobEmployeeVM model, DateTime data, string? userId, int id)
    {
        var endJob = _mapper.Map<JobEmployee>(model);
        var JobId = model.JobId;
        await _jobEmployeeRepository.StopJobEmployee(endJob, id, userId, JobId);
    }


    public async Task<ListJobEmployeeVM> GetAllJobEmployeesAsync() 
    {
        var jobEmployees = await _jobEmployeeRepository.GetAllAsync();
        var jobEmployeeVm = _mapper.Map<List<JobEmployeeForListVM>>(jobEmployees);
        return new ListJobEmployeeVM
        {
            JobEmployeeForListVms = jobEmployeeVm
        };
    }


    public async Task<List<DetailsJobEmployeeVM>> GetAllJobsEmployeeDetails(int id)
    {
        var operation = await _operationRepository.GetOperationFromDB(id);
        var jel = _jobEmployeeRepository.GetAllJobsEmployeeByIdFromDB(id).ToList();
        var jobEmployeeList = _mapper.Map<List<DetailsJobEmployeeVM>>(jel);
        foreach (var jobEmployee in jobEmployeeList)
        {
            var user = _employeeRepository.GetEmployee(jobEmployee.CurrentWorkerId);

            if (user != null)
            {
                jobEmployee.EmployeeName = user.Name;
                jobEmployee.EmployeeSurname = user.Surname;
            }

            double totalTime = Math.Round((jobEmployee.EndTime - jobEmployee.StartTime).TotalMinutes);
            double expectedTime = operation.EstimatedTimePerUnit.TotalMinutes * jobEmployee.CompletedQuantity;

            double efficiencyPercentage = totalTime > 0
                ? Math.Round(expectedTime / totalTime * 100)
                : 0;

            jobEmployee.Efficiency = efficiencyPercentage;
        }


        return jobEmployeeList;
    }

    public async Task<DetailsJobEmployeeVM> GetJobEmployeeDetailsAsync(int id)
    {
        var jobEmployee = await _jobEmployeeRepository.GetByIdAsync(id);
        return _mapper.Map<DetailsJobEmployeeVM>(jobEmployee);
    }



    public void EditJobEmployee(EditJobEmployeeVM model)
    {
        var jobEmployee = _mapper.Map<JobEmployee>(model);
        _jobEmployeeRepository.EditJobEmployee(jobEmployee);
    }
}














//  public void StartJobEmployee(DateTime data, string? userId, int id)
// {
//     var job = new JobEmployee()
//     {
//         CompletedQuantity = 0,
//         EmployeeComments = "",
//         MissingQuantity = 0,
//         StartTime = data,
//         CurrentWorkerId = userId,
//         OperationId = id
//     };
//
//     _jobEmployeeRepository.StartJobEmployee(job);
// }


// public List<JobEmployee> GetAllJobsEmployee()
// {
//     var jel = _jobEmployeeRepository.GetAllJobsEmployeeFromDB();
//     return _mapper.Map<List<JobEmployee>>(jel);
// }
//

// public DetailsJobEmployeeVM GetJobEmployeeDetailById(int id)
// {
//     var jobEmployee = _jobEmployeeRepository.GetJobEmployeeFromDB(id);
//     var jobEmployeeVM = _mapper.Map<DetailsJobEmployeeVM>(jobEmployee);
//     return jobEmployeeVM;
// }

// public async Task EditJobEmployeeAsync(EditJobEmployeeVM model)
// {
//     var jobEmployee = _mapper.Map<JobEmployee>(model);
//     await _jobEmployeeRepository.UpdateAsync(jobEmployee);
// }