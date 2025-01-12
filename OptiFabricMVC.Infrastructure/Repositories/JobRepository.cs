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
        return _context.Jobs.AsQueryable();
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
        var employee = _context.ApplicationUsers.FirstOrDefault(e => e.Id == jobEmployee.CurrentWorkerId);
        jobEmployee.CurrentWorker = employee;
        var job = _context.Jobs.FirstOrDefault(j => j.Id == jobEmployee.JobId);
        job.IsCompleted = JobStatus.InProgress;
        job.ActivEmployeeJob = true;
        _context.JobEmployees.Add(jobEmployee);
        _context.SaveChanges();
    }

    public void StopJobEmployee(JobEmployee jobEmployee, int id)
    {
        var job = _context.Jobs.FirstOrDefault(j => j.Id == id);
        var jobEmp = _context.JobEmployees.FirstOrDefault(je => je.JobId == id);
        job.TotalCompletedQuantity = job.TotalCompletedQuantity + jobEmployee.CompletedQuantity;
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
        _context.SaveChanges();
    }
}