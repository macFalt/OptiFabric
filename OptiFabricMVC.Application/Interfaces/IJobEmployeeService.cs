using OptiFabricMVC.Application.ViewModels.JobEmployeeVM;
using OptiFabricMVC.Application.ViewModels.JobVM;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.Interfaces;

public interface IJobEmployeeService
{
    Task<List<JobEmployeeForListVM>> GetAllJobEmployeesByJobIdAsync(int JobId);
    Task<DetailsJobEmployeeVM> GetJobEmployeeDetailsAsync(int id);
    Task EditJobEmployee(EditJobEmployeeVM model);
    Task<List<DetailsJobEmployeeVM>> GetAllJobsEmployeeDetails(int operationId);
   Task StartJobEmployee2(DateTime data, string? userId, int id, int selectedMachineId, int jobId);

    Task StopJobEmployee(EndJobEmployeeVM model, DateTime data, string? userId, int id);
}



















// void StartJobEmployee(DateTime data, string? userId,int id);
//List<DetailsJobEmployeeVM> GetAllJobsEmployeeDetails(int id);
//List<JobEmployee> GetAllJobsEmployee();
// DetailsJobEmployeeVM GetJobEmployeeDetailById(int id);
