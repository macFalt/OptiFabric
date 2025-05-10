using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

    public async Task<JobEmployee> GetActiveJobEmployeeByUserId(string userId)
    {
        return await _context.JobEmployees.FirstOrDefaultAsync(x=>x.IsActive==true && x.CurrentWorkerId==userId);
    }
    
    public async Task StartJobEmployee(JobEmployee jobEmployee)
    {
        _context.JobEmployees.Add(jobEmployee);
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



