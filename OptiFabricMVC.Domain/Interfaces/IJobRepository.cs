using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Domain.Interfaces;

public interface IJobRepository
{
    IQueryable<Job> GetAllJobsFromDB();
    int AddJobToDB(Job job);
    void StartJobEmployee(JobEmployee jobEmployee);
    void StopJobEmployee(JobEmployee jobEmployee,int id);
}