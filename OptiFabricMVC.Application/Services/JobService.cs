using System.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
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
    private readonly IOperationRepository _operationRepository;

    public JobService(IJobRepository jobRepository, IMapper iMapper, IEmployeeRepository employeeRepository, IOperationRepository operationRepository)
    {
        _jobRepository = jobRepository;
        _mapper = iMapper;
        _employeeRepository = employeeRepository;
        _operationRepository = operationRepository;
    }

    public async Task<int> AddJob(AddNewJobVM model)
    {
        var job = _mapper.Map<Job>(model);
        var jobId= await _jobRepository.AddJobToDB(job);
        return jobId;
    }
    
    
    public async Task<AddNewJobVM> GetSelectedJobAsync(int id)
    {
        var job = await _jobRepository.GetByIdAsync(id);
        return _mapper.Map<AddNewJobVM>(job);
    }


    public async Task EditJobAsync(EditJobVM model)
    {
        var job=_mapper.Map<Job>(model);
        await _jobRepository.UpdateAsync(job);
    }
    
    
    public async Task<ListJobsVM> GetAllJobsAsync(int pageSize, int pageNo, string searchString)
    {
        var query = _jobRepository.GetAllJobsFromDB();
        if (!string.IsNullOrWhiteSpace(searchString) && DateTime.TryParse(searchString, out DateTime parsedDate))
            {
                query = query.Where(m => m.CreatedAt.Date == parsedDate.Date).ToList();
            }
        var count = query.Count();

        var jobsToShow =  query
            .OrderBy(m => m.CompletedAt)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .Select(m => _mapper.Map<JobsForListVM>(m))
            .ToList();

        var jobsList = new ListJobsVM()
        {
            PageSize = pageSize,
            CurrentPage = pageNo,
            SearchString = searchString,
            JobsForList = jobsToShow,
            Count = count
        };

        return jobsList;
    }
    
    

    // public ListJobsVM GetAllJobs(int pageSize, int pageNo, string searchString)
    // {
    //     var jobsQuery = _jobRepository.GetAllJobsFromDB();
    //     if (!string.IsNullOrWhiteSpace(searchString) && DateTime.TryParse(searchString, out DateTime parsedDate))
    //     {
    //         jobsQuery = jobsQuery.Where(m => m.CreatedAt.Date == parsedDate.Date);
    //     }
    //
    //     var jobs = jobsQuery
    //         .ProjectTo<JobsForListVM>(_mapper.ConfigurationProvider)
    //         .ToList();
    //     
    //     var jobsList = new ListJobsVM()
    //     {
    //         PageSize = pageSize,
    //         CurrentPage = pageNo,
    //         SearchString = searchString,
    //         JobsForList = jobs,
    //         Count = jobs.Count
    //     };
    //     return jobsList;
    // }



   
}







// public AddNewJobVM GetSelectedJob(int id)
// {
//     var job = _jobRepository.GetSelectedJobFromDB(id);
//     var jobVM = _mapper.Map<AddNewJobVM>(job);
//     return jobVM;
// }

// public void EditJob(AddNewJobVM model)
// {
//     var job = _mapper.Map<Job>(model);
//     _jobRepository.EditJobDB(job);
// }