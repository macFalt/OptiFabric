using AutoMapper;
using AutoMapper.QueryableExtensions;
using OptiFabricMVC.Application.Interfaces;
using OptiFabricMVC.Application.ViewModels.JobVM;
using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.Services;

public class JobService: IJobService
{
    private readonly IJobRepository _jobRepository;
    private readonly IMapper _mapper;

    public JobService(IJobRepository jobRepository,IMapper iMapper)
    {
        _jobRepository = jobRepository;
        _mapper = iMapper;
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

    public void StartJobEmployee(DateTime data, string? userId,int id)
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
        _jobRepository.StopJobEmployee(endJob,id);
    }


}