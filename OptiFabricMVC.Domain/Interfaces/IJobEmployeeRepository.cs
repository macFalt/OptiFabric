using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Domain.Interfaces;

public interface IJobEmployeeRepository : IGenericRepository<JobEmployee,int>
{
    Task StartJobEmployee(JobEmployee jobEmployee);
    Task StopJobEmployee(JobEmployee jobEmployee, int id, string userId, int JobId);
    Task<List<JobEmployee>> GetAllJobsEmployeeByOperationIdFromDB(int id);
    Task<List<JobEmployee>> GetAllJobEmployeeByJobId(int JobId);
    Task UpdateJobEmployee(JobEmployee jobEmployee);
    Task SaveChangesAsync();
    
    

}





















//void StartJobEmployee(JobEmployee jobEmployee);
//void StopJobEmployee(JobEmployee jobEmployee,int id,string userId,int JobId);
// IQueryable<JobEmployee> GetAllJobsEmployeeFromDB();
// JobEmployee GetJobEmployeeFromDB(int id);
// Task EditJobEmployee(JobEmployee jobEmployee);
