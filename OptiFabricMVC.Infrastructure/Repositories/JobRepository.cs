using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Infrastructure.Repositories;

public class JobRepository : IJobRepository
{
    private readonly Context _context;

    public JobRepository(Context context)
    {
        _context = context;
    }

    public IQueryable<Job> GetAllJobsFromDB()
    {
        // var jobs = _context.Jobs.ToList();
        // var jel = _context.JobEmployees.ToList();
        //
        // foreach (var job in jobs)
        // {
        //     job.TotalCompletedQuantity = 0;
        //     job.TotalCompletedQuantity = jel
        //         .Where(emp => emp.JobId == job.Id)  
        //         .Sum(emp => emp.CompletedQuantity); 
        //     job.TotalMissingQuantity=jel
        //         .Where(emp => emp.JobId == job.Id)
        //         .Sum(emp => emp.MissingQuantity);
        //     
        // }
        // _context.SaveChanges();    

        return _context.Jobs.AsQueryable();
    }
    
    public IQueryable<JobEmployee> GetAllJobsEmployeeFromDB()
    {
        return _context.JobEmployees.AsQueryable();
    }

    public IQueryable<JobEmployee> GetAllJobsEmployeeByIdFromDB(int id)
    {
        var JobEmployeeByJobId = _context.JobEmployees.Where(x => x.JobId == id);
        return JobEmployeeByJobId;
    }

    public JobEmployee GetJobEmployeeFromDB(int id)
    {
        return _context.JobEmployees.FirstOrDefault(je => je.Id == id);
    }

    public void EditJobEmployee(JobEmployee jobEmployee)
    {
        var jobEmp = _context.JobEmployees.FirstOrDefault(je => je.Id==jobEmployee.Id);
        jobEmp.CompletedQuantity = jobEmployee.CompletedQuantity;
        jobEmp.MissingQuantity = jobEmployee.MissingQuantity;
        // var job = _context.Jobs.FirstOrDefault(j => j.Id == jobEmployee.JobId);
        // job.TotalCompletedQuantity = job.TotalCompletedQuantity + jobEmp.CompletedQuantity;
        // job.TotalMissingQuantity = job.TotalMissingQuantity + jobEmp.MissingQuantity;
        
        var jobs = _context.Jobs.ToList();
        var jel = _context.JobEmployees.ToList();

        foreach (var job in jobs)
        {
            job.TotalCompletedQuantity = 0;
            job.TotalCompletedQuantity = jel
                .Where(emp => emp.JobId == job.Id)  
                .Sum(emp => emp.CompletedQuantity); 
            job.TotalMissingQuantity=jel
                .Where(emp => emp.JobId == job.Id)
                .Sum(emp => emp.MissingQuantity);
            
        }
        _context.SaveChanges();  
        // _context.SaveChanges();    
    }

    public int AddJobToDB(Job job)
    {
        var productFromDb = _context.Products.FirstOrDefault(p => p.Id == job.Product.Id);
        job.Product = productFromDb;
        job.IsCompleted = JobStatus.NotStarted;
        _context.Jobs.Add(job);
        _context.SaveChanges();
        return job.Id;
    }

    public void StartJobEmployee(JobEmployee jobEmployee)
    {
        jobEmployee.IsActive = true;
        var job = _context.Jobs.FirstOrDefault(j => j.Id == jobEmployee.JobId);
        job.IsCompleted = JobStatus.InProgress;
        job.ActivEmployeeJob = true;
        _context.JobEmployees.Add(jobEmployee);
        _context.SaveChanges();
    }


    public void StopJobEmployee(JobEmployee jobEmployee, int id,string userId)
    {
        var job = _context.Jobs.FirstOrDefault(j => j.Id == id);
        var jobEmp = _context.JobEmployees.FirstOrDefault(je => je.IsActive == true && je.CurrentWorkerId==userId);
        
        job.TotalCompletedQuantity = job.TotalCompletedQuantity + jobEmployee.CompletedQuantity;
        job.TotalMissingQuantity = job.TotalMissingQuantity + jobEmployee.MissingQuantity;
        job.ActivEmployeeJob = false;
        if (job.TotalCompletedQuantity == job.RequiredQuantity)
        {
            job.IsCompleted = JobStatus.Completed;
            job.CompletedAt = jobEmployee.EndTime;
        }
        else
        {
            job.IsCompleted = JobStatus.InProgress;
        }

        jobEmp.EndTime = jobEmployee.EndTime;
        jobEmp.CompletedQuantity = jobEmployee.CompletedQuantity;
        jobEmp.EmployeeComments = jobEmployee.EmployeeComments;
        jobEmp.MissingQuantity = jobEmployee.MissingQuantity;
        jobEmp.IsActive = false;
        _context.SaveChanges();
    }

    public Job GetSelectedJobFromDB(int id)
    {

        var job = _context.Jobs.FirstOrDefault(e => e.Id == id);
        return job;
        
    }

    public void EditJobDB(Job job)
    {
        
        _context.Jobs.Update(job);
        _context.SaveChanges();
        
    }
}