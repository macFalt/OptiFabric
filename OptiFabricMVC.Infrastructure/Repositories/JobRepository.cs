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
        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();
        return job.Id;
    }

    public IQueryable<Job> GetAllJobsFromDB()
    {
        var jobs = _context.Jobs
            .Include(j => j.Operations)
            .Include(j => j.Product)
            .AsQueryable();
        
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