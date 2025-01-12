using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    

    public EmployeeControler(IEmployeeService employeeService, UserManager<ApplicationUser> userManager, IShiftService shiftService)
    {
        _employeeService = employeeService;
        _userManager = userManager;
        _shiftService = shiftService;
    }
    
    // GET
    public IActionResult Index(int pageSize = 10, int pageNo = 1, string searchString = "")
    {
        var model = _employeeService.GetAllEmployee(pageSize, pageNo,searchString);
        return View(model);
    }

    [HttpGet]
    public IActionResult AddEmployee()
    {
        var model = new NewEmployeeVM();
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


    public IActionResult Details(string id)
    {
        var employee = _employeeService.GetEmployeeDetail(id);
        
        return View(employee);
    }

    public IActionResult Delete(string id)
    {
        _employeeService.DeleteEmployee(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult EditEmployee(string id)
    {
        var employee = _employeeService.GetEmployeeDetail(id);
        return View(employee);
    }

    [HttpPost]
    public IActionResult EditEmployee(EditEmployeeVM model)
    {
        _employeeService.EditEmployee(model);
        return RedirectToAction("Index");
    }

    public IActionResult WorkingHours(string id,int pageSize = 10, int pageNo = 1, string searchString = "")
    {
        var shift = _shiftService.GetAllShifts(id,pageSize, pageNo,searchString);
        return View(shift);
    }
}