using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Domain.Interfaces;

public interface IJobEmployeeRepository : IGenericRepository<JobEmployee,int>
{
    Task StartJobEmployee(JobEmployee jobEmployee);
    Task<List<JobEmployee>> GetAllJobsEmployeeByOperationIdFromDB(int id);
    Task<List<JobEmployee>> GetAllJobEmployeeByJobId(int JobId);
    Task UpdateJobEmployee(JobEmployee jobEmployee);
    Task SaveChangesAsync();
    Task<JobEmployee> GetActiveJobEmployeeByUserId(string userId);



}












