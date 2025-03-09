using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Domain.Interfaces;

public interface IJobRepository
{
    IQueryable<Job> GetAllJobsFromDB();
    int AddJobToDB(Job job);
    void StartJobEmployee(JobEmployee jobEmployee);
    void StopJobEmployee(JobEmployee jobEmployee,int id,string userId);
    Job GetSelectedJobFromDB(int id);
    void EditJobDB(Job job);

    IQueryable<JobEmployee> GetAllJobsEmployeeFromDB();
    IQueryable<JobEmployee> GetAllJobsEmployeeByIdFromDB(int id);
    JobEmployee GetJobEmployeeFromDB(int id);
    void EditJobEmployee(JobEmployee jobEmployee);
    bool IsMachineBusy(int selectedMachineId);
}