using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Domain.Interfaces;

public interface IEmployeeRepository
{
     string AddEmployee(ApplicationUser employee);

    IQueryable<ApplicationUser> GetAllEmployeeFromDB();

    Task<ApplicationUser> GetEmployee(string id);
    void DeleteEmployee(string id);
    void UpdateEmployee(ApplicationUser employee);
    void DeleteShiftFromDB(int id);
}