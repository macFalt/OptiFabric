using OptiFabricMVC.Application.ViewModels.JobVM;

namespace OptiFabricMVC.Application.Interfaces;

public interface IJobService
{
    ListJobsVM GetAllJobs(int pageSize, int pageNo, string searchString);
    int AddJob(AddNewJobVM model);
    void StartJobEmployee(DateTime data, string? userId,int id);
    void StopJobEmployee(EndJobEmployeeVM model,DateTime data, string? userId, int id);
}