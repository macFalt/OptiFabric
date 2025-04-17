using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Domain.Interfaces;

public interface IJobEmployeeRepository : IGenericRepository<JobEmployee,int>
{
    Task StartJobEmployee(JobEmployee jobEmployee);
    Task StopJobEmployee(JobEmployee jobEmployee, int id, string userId, int JobId);
    IQueryable<JobEmployee> GetAllJobsEmployeeByIdFromDB(int id);
    void EditJobEmployee(JobEmployee jobEmployee);
    
}





















//void StartJobEmployee(JobEmployee jobEmployee);
//void StopJobEmployee(JobEmployee jobEmployee,int id,string userId,int JobId);
// IQueryable<JobEmployee> GetAllJobsEmployeeFromDB();
// JobEmployee GetJobEmployeeFromDB(int id);
// Task EditJobEmployee(JobEmployee jobEmployee);
