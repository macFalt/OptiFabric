using Microsoft.EntityFrameworkCore;
using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Infrastructure.Repositories;

public class JobRepository : GenericRepository<Job,int> ,IJobRepository
{
    private readonly Context _context;

    public JobRepository(Context context) : base(context)
    {
        _context = context;
    }

    public async Task<int> AddJobToDB(Job job)
    {
        var productFromDb = await _context.Products.FirstOrDefaultAsync(p => p.Id == job.ProductId);
        job.Product = productFromDb;
        job.JobStatus = JobStatus.NotStarted;
        var operationPatterns = _context.OperationPatterns.Where(op => op.ProductId == productFromDb.Id).ToList();
        job.Operations = operationPatterns.Select(opPattern => new Operation
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
        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();
        return job.Id;
    }

    public List<Job> GetAllJobsFromDB()
    {
        var jobs = _context.Jobs
            .Include(j=>j.Operations)
            .Include(j=>j.Product)
            .ToList();
        
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
        
            if ((job.TotalCompletedQuantity+job.TotalMissingQuantity) >= job.RequiredQuantity)
            {
                job.JobStatus = JobStatus.Completed;
                job.CompletedAt = DateTime.Now;
            }
            else
            {
                job.JobStatus = JobStatus.InProgress;
            }
        }
        return jobs;
    }
    
    

    


    
    
}







// public Job GetSelectedJobFromDB(int id)
// {
//
//     var job = _context.Jobs.FirstOrDefault(e => e.Id == id);
//     return job;
//         
// }
//
// public void EditJobDB(Job job)
// {
//         
//     _context.Jobs.Update(job);
//     _context.SaveChanges();
//         
// }
// public IQueryable<Job> GetAllJobsFromDB()
// {
//     var jobs= _context.Jobs.AsQueryable();
//     return jobs;
// }

// }