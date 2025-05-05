using Microsoft.EntityFrameworkCore;
using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Infrastructure.Repositories;

public class JobEmployeeRepository : GenericRepository<JobEmployee,int> ,IJobEmployeeRepository
{
    private readonly Context _context;

    public JobEmployeeRepository(Context context) : base(context)
    {
        _context = context;
    }
    
    
    public async Task<List<JobEmployee>> GetAllJobsEmployeeByOperationIdFromDB(int id)
    {
        return await _context.JobEmployees
            .Where(x => x.OperationId == id)
            .ToListAsync();
    }
    
    public async Task<List<JobEmployee>> GetAllJobEmployeeByJobId(int JobId)
    {
        return await _context.JobEmployees
            .Where(x=>x.JobId==JobId)
            .ToListAsync();
    }

    
    
    public async Task StartJobEmployee(JobEmployee jobEmployee)
    {
        var operation = await _context.Operations.FirstOrDefaultAsync(op => op.Id == jobEmployee.OperationId);
        if (operation != null)
        {
            operation.OperationStatus = OperationStatus.InProgress;
        }
        jobEmployee.IsActive = true;
        
        var job = await _context.Jobs.FirstOrDefaultAsync(j => j.Id == jobEmployee.JobId);
        var machine = await _context.Machines.FirstOrDefaultAsync(machine => machine.Id == jobEmployee.MachineId);
        machine.Status= MachineStatus.Zajęta;
        job.JobStatus = JobStatus.InProgress;
        job.ActivEmployeeJob = true;
        _context.JobEmployees.Add(jobEmployee);
        await _context.SaveChangesAsync();
    }


    public async Task StopJobEmployee(JobEmployee jobEmployee, int id,string userId,int jobId)
    {
        var job = await _context.Jobs.FirstOrDefaultAsync(j => j.Id == jobId);
        var jobEmp = await _context.JobEmployees.FirstOrDefaultAsync(je => je.IsActive == true && je.CurrentWorkerId==userId);
        var operation = await _context.Operations.FirstOrDefaultAsync(op => op.Id == id);
        
        if (operation != null)
        {
            operation.CompletedQuantity+=jobEmployee.CompletedQuantity;
            operation.MissingQuantity+=jobEmployee.MissingQuantity; 
            operation.OperationStatus = operation.CompletedQuantity+operation.MissingQuantity==operation.RequiredQuantity ? OperationStatus.Completed : OperationStatus.InProgress;
        }

        if (jobEmp != null)
        {
            jobEmp.EndTime = jobEmployee.EndTime;
            jobEmp.CompletedQuantity = jobEmployee.CompletedQuantity;
            jobEmp.EmployeeComments = jobEmployee.EmployeeComments;
            jobEmp.MissingQuantity = jobEmployee.MissingQuantity;
            jobEmp.IsActive = false; 
        }
        
        var machine = await _context.Machines.FirstOrDefaultAsync(machine => machine.Id == jobEmp.MachineId);
        if (machine != null)
        {
            machine.Status= MachineStatus.Wolna;
        }

        var lastOperation = await _context.Operations
            .Where(jobid=>jobid.JobId == jobId)
            .OrderByDescending(op => op.Id)
            .FirstOrDefaultAsync();


        if (lastOperation != null)
        {
            job.TotalCompletedQuantity = lastOperation.CompletedQuantity;
            job.TotalMissingQuantity = lastOperation.MissingQuantity;
        }
        
        if ((job.TotalCompletedQuantity+job.TotalMissingQuantity) >= job.RequiredQuantity)
        {
            job.JobStatus = JobStatus.Completed;
            job.CompletedAt = jobEmployee.EndTime;
        }
        else
        {
            job.JobStatus = JobStatus.InProgress;
        }
        
        await _context.SaveChangesAsync();
    }
    
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    
    
    public async Task UpdateJobEmployee(JobEmployee jobEmployee)
    {
        _context.Entry(jobEmployee).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}



// public IQueryable<JobEmployee> GetAllJobsEmployeeFromDB()
// {
//     return _context.JobEmployees.AsQueryable();
// }


// public JobEmployee GetJobEmployeeFromDB(int id)
// {
//     return _context.JobEmployees.FirstOrDefault(je => je.Id == id);
// }