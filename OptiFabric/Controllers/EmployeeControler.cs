using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptiFabricMVC.Application.Interfaces;
using OptiFabricMVC.Application.Services;
using OptiFabricMVC.Application.ViewModels.EmployeeVM;
using OptiFabricMVC.Domain.Model;

namespace OptiFabric.Controllers;
[Authorize(Roles = "Manager")]

public class EmployeeControler : Controller
{
    private readonly IEmployeeService _employeeService;
    private readonly IShiftService _shiftService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    

    public EmployeeControler(IEmployeeService employeeService, UserManager<ApplicationUser> userManager, IShiftService shiftService, RoleManager<IdentityRole> roleManager)
    {
        _employeeService = employeeService;
        _userManager = userManager;
        _shiftService = shiftService;
        _roleManager = roleManager;
    }
    
    // GET
    public async Task<IActionResult> Index(int pageSize = 10, int pageNo = 1, string searchString = "")
    {
        var model = _employeeService.GetAllEmployee(pageSize, pageNo,searchString);
        foreach (var employee in model.EmployeeForListVms)
        {
            var user = await _userManager.FindByIdAsync(employee.Id);
            if (user != null)
            {
                employee.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            }
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> AddEmployee()
    {
        var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
        var model = new NewEmployeeVM
        {
            AvailableRoles = roles
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddEmployee(NewEmployeeVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var result = await _employeeService.AddEmployeeAsync(model);
        if (result.Succeeded)
        {
            return RedirectToAction("Index");
        }
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return View(model);
    }


    public async Task<IActionResult> Details(string id)
    {
        var employee = await _employeeService.GetEmployeeDetail(id);
        
        return View(employee);
    }

    public IActionResult Delete(string id)
    {
        _employeeService.DeleteEmployee(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> EditEmployee(string id)
    {
        var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
        var employee =await _employeeService.GetEmployeeDetail(id);
            var user = await _userManager.FindByIdAsync(employee.Id);
            if (user != null)
            {
                employee.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            }
        employee.AvailabeRoles = roles;
        return View(employee);
    }

    [HttpPost]
    public async Task<IActionResult> EditEmployee(EditEmployeeVM model)
    {
        await _employeeService.EditEmployee(model);
        return RedirectToAction("Index");
    }

    public IActionResult WorkingHours(string id,int pageSize = 10, int pageNo = 1, string searchString = "")
    {
        var shift = _shiftService.GetAllShifts(id,pageSize, pageNo,searchString);
        return View(shift);
    }

    public IActionResult DeleteShift(int id)
    {
        _employeeService.DeleteShift(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> AddRole()
    {
        var model=new AddNewRolesVM();
        return View(model);
    }

    
    [HttpPost]
    public async Task<IActionResult> AddRole(AddNewRolesVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var newRole = new IdentityRole(model.Name); // Tworzenie nowej roli
        var result = await _roleManager.CreateAsync(newRole); // Dodanie roli do bazy

        if (result.Succeeded)
        {
            return RedirectToAction("Index"); // Powrót do listy ról
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }

        return View(model);
    }
}