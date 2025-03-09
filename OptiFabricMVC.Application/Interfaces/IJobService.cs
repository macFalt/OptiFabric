using OptiFabricMVC.Application.ViewModels.JobVM;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.Interfaces;

public interface IJobService
{
    ListJobsVM GetAllJobs(int pageSize, int pageNo, string searchString);
    int AddJob(AddNewJobVM model);
    void StartJobEmployee(DateTime data, string? userId,int id);
    void StopJobEmployee(EndJobEmployeeVM model,DateTime data, string? userId, int id);
    AddNewJobVM GetSelectedJob(int id);
    void EditJob(AddNewJobVM model);
    List<JobEmployee> GetAllJobsEmployee();
    List<DetailsJobEmployeeVM> GetAllJobsEmployeeDetails(int id);
    DetailsJobEmployeeVM GetJobEmployeeDetailById(int id);
    void EditJobEmployee(DetailsJobEmployeeVM model);
    void StartJobEmployee2(DateTime data, string? userId, int id, int selectedMachineId);
}