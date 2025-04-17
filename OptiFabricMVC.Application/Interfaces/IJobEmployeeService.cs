using OptiFabricMVC.Application.ViewModels.JobEmployeeVM;
using OptiFabricMVC.Application.ViewModels.JobVM;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.Interfaces;

public interface IJobEmployeeService
{
    Task<ListJobEmployeeVM> GetAllJobEmployeesAsync();
    Task<DetailsJobEmployeeVM> GetJobEmployeeDetailsAsync(int id);
    // Task EditJobEmployeeAsync(EditJobEmployeeVM model);
    void EditJobEmployee(EditJobEmployeeVM model);
    Task<List<DetailsJobEmployeeVM>> GetAllJobsEmployeeDetails(int id);
   // void StartJobEmployee2(DateTime data, string? userId, int id, int selectedMachineId,int JobId);
   Task StartJobEmployee2(DateTime data, string? userId, int id, int selectedMachineId, int jobId);
    //void StopJobEmployee(EndJobEmployeeVM model, DateTime data, string? userId, int id);

    Task StopJobEmployee(EndJobEmployeeVM model, DateTime data, string? userId, int id);
}



















// void StartJobEmployee(DateTime data, string? userId,int id);
//List<DetailsJobEmployeeVM> GetAllJobsEmployeeDetails(int id);
//List<JobEmployee> GetAllJobsEmployee();
// DetailsJobEmployeeVM GetJobEmployeeDetailById(int id);
