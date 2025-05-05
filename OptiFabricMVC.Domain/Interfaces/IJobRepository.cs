using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Domain.Interfaces;

public interface IJobRepository : IGenericRepository<Job,int>
{
    Task<int> AddJobToDB(Job job);
    IQueryable<Job> GetAllJobsFromDB();



}


















// void EditJobDB(Job job);
// Job GetSelectedJobFromDB(int id);
// IQueryable<Job> GetAllJobsFromDB();