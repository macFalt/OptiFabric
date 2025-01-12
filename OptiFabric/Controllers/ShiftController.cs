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
        //ViewBag.CurrentDateTime = DateTime.UtcNow.ToString("o"); // ISO 8601 format: "YYYY-MM-DDTHH:mm:ss.fffZ"
        ViewBag.CurrentDateTime = DateTime.Now; // ISO 8601 format: "YYYY-MM-DDTHH:mm:ss.fffZ"
        return View();
    }

    public IActionResult StartShift()
    {
        var data = DateTime.Now;
        var userId = _userManager.GetUserId(User);
        
        _shiftService.StartShift(data,userId);
        return View("Index");
    }

    public IActionResult EndShift()
    {
        var data = DateTime.Now;
        var userId = _userManager.GetUserId(User);
        
        _shiftService.EndShift(data,userId);
        return View("Index");
    }
}