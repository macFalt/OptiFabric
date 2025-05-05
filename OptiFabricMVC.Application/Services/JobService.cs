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
    private readonly IProductRepository _productRepository;

    public JobService(IJobRepository jobRepository, IMapper iMapper, IEmployeeRepository employeeRepository,
        IOperationRepository operationRepository, IProductRepository productRepository)
    {
        _jobRepository = jobRepository;
        _mapper = iMapper;
        _employeeRepository = employeeRepository;
        _operationRepository = operationRepository;
        _productRepository = productRepository;
    }

    public async Task<int> AddJob(AddNewJobVM model)
    {
        var job = _mapper.Map<Job>(model);
        job.JobStatus = JobStatus.NotStarted;
        
        var jobProduct= await _productRepository.GetByIdAsync(job.ProductId);
        if (jobProduct == null)
            throw new Exception("Job Product not found");
        job.Product = jobProduct;
        
        var operationP= _operationRepository.GetAllOperationsPatternFromDB(job.ProductId);
        if (operationP == null)
            throw new Exception("Operation Pattern not found");
        
        
        job.Operations = operationP.Select(opPattern => new Operation
        {
            Name = opPattern.Name,
            Description = opPattern.Description,
            EstimatedTimePerUnit = opPattern.EstimatedTimePerUnit,
            RequiredQuantity = job.RequiredQuantity,
            CompletedQuantity = 0,
            MissingQuantity = 0,
            OperationStatus = OperationStatus.NotStarted,
            OperationPatternId = opPattern.Id
        }).ToList();        
        
        var jobId = await _jobRepository.AddJobToDB(job);
        return jobId;
    }


    public async Task<AddNewJobVM> GetSelectedJobAsync(int id)
    {
        var job = await _jobRepository.GetByIdAsync(id);
        return _mapper.Map<AddNewJobVM>(job);
    }


    public async Task EditJobAsync(EditJobVM model)
    {
        var job = _mapper.Map<Job>(model);
        await _jobRepository.UpdateAsync(job);
    }


    public async Task<ListJobsVM> GetAllJobsAsync(int pageSize, int pageNo, string searchString)
    {
        var query = _jobRepository.GetAllJobsFromDB();

        if (!string.IsNullOrWhiteSpace(searchString) && DateTime.TryParse(searchString, out DateTime parsedDate))
        {
            query = query.Where(m => m.CreatedAt.Date == parsedDate.Date);
        }

        var count = await query.CountAsync();

        var jobs = await query
            .OrderBy(m => m.CompletedAt)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        foreach (var job in jobs)
        {
            var lastOperation = job.Operations
                .OrderByDescending(op => op.Id)
                .FirstOrDefault();

            if (lastOperation != null)
            {
                job.TotalCompletedQuantity = lastOperation.CompletedQuantity;
                job.TotalMissingQuantity = lastOperation.MissingQuantity;
            }

            if ((job.TotalCompletedQuantity + job.TotalMissingQuantity) >= job.RequiredQuantity)
            {
                job.JobStatus = JobStatus.Completed;
                job.CompletedAt = DateTime.Now;
            }
        }

        var jobsToShow = jobs
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