using Microsoft.AspNetCore.Identity;
using OptiFabricMVC.Application.ViewModels.EmployeeVM;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.Interfaces;

public interface IEmployeeService
{
    Task<IdentityResult> AddEmployeeAsync(NewEmployeeVM model);
    ListEmployeeVM GetAllEmployee(int pageSize, int pageNo, string searchString);
    Task<EmployeeForListVM> GetEmployeeDetail(string id);
    void DeleteEmployee(string id);
    Task EditEmployee(EditEmployeeVM model);
    void DeleteShift(int id);
}