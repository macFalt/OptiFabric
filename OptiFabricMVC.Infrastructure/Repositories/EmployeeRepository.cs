using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Infrastructure.Repositories;

public class EmployeeRepository: IEmployeeRepository
{
    public readonly Context _context;

    public EmployeeRepository(Context context)
    {
        _context = context;
    }

    public string AddEmployee(ApplicationUser employee)
    {
        _context.Add(employee);
        _context.SaveChanges();
        return employee.Id;
    }

    public IQueryable<ApplicationUser> GetAllEmployeeFromDB()
    {
        var manager = _context.UserRoles
            .Where(user => user.RoleId == "Manager")
            .Select(user => user.UserId)
            .ToList();
        
        var employees = _context.ApplicationUsers
            .Where(user=>!manager.Contains(user.Id))
            .AsQueryable();

        return employees;
    }

    public ApplicationUser GetEmployee(string id)
    {
        var employee = _context.ApplicationUsers.FirstOrDefault(e => e.Id == id);
        return employee;
    }

    public void DeleteEmployee(string id)
    {
        var employee = _context.ApplicationUsers.Find(id);
        if (employee != null)
        {
            _context.ApplicationUsers.Remove(employee);
            _context.SaveChanges();
        }
    }

    public void UpdateEmployee(ApplicationUser employee)
    {
        var existingEmployee = _context.ApplicationUsers.FirstOrDefault(e => e.Id == employee.Id);
        existingEmployee.Name = employee.Name;
        existingEmployee.Surname = employee.Surname;
        existingEmployee.Position = employee.Position;
        _context.SaveChanges();
        
        

    }
}