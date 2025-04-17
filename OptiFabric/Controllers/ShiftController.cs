using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OptiFabricMVC.Application.Interfaces;
using OptiFabricMVC.Domain.Model;

namespace OptiFabric.Controllers;

public class ShiftController : Controller
{
    private readonly IShiftService _shiftService;
    private readonly UserManager<ApplicationUser> _userManager;

    public ShiftController(IShiftService shiftService, UserManager<ApplicationUser> userManager)
    {
        _shiftService = shiftService;
        _userManager = userManager;
    }
    // GET
    public IActionResult Index()
    {

        ViewBag.CurrentDateTime = DateTime.Now; 
        return View();
    }

    public async Task<IActionResult> StartShift()
    {
        var data = DateTime.Now;
        var userId = _userManager.GetUserId(User);
        
        await _shiftService.StartShift(data,userId);
        return View("Index");
    }

    public async Task<IActionResult> EndShift()
    {
        var data = DateTime.Now;
        var userId = _userManager.GetUserId(User);
        
        await _shiftService.EndShift(data,userId);
        return View("Index");
    }
}