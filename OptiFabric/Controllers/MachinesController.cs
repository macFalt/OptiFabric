using Microsoft.AspNetCore.Mvc;
using OptiFabricMVC.Application.Interfaces;
using OptiFabricMVC.Application.ViewModels.MachinesVM;

namespace OptiFabric.Controllers;

public class MachinesController : Controller
{
    private readonly IMachineService _machineService;

    public MachinesController(IMachineService machineService)
    {
        _machineService = machineService;
    }
    // GET
    public async Task<IActionResult> Index(int pageSize = 10, int pageNo = 1, string searchString = "")
    {
        return View( await _machineService.GetAllMachines(pageSize,pageNo,searchString));
    }

    [HttpGet]
    public IActionResult AddMachine()
    {
        var model = new MachinesForListVM();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddMachine(MachinesForListVM model)
    {
        await  _machineService.AddMachineAsync(model);
        return RedirectToAction("Index");
    } 

    [HttpGet]
    public async Task<IActionResult> EditMachine(int id)
    {
        var model = await _machineService.GetDetailsAsync(id);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditMachine(EditMachineVM model)
    {
        await _machineService.EditMachineAsync(model);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(int id)
    {
        var machines = await _machineService.GetDetailsAsync(id);
        return View(machines);
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _machineService.DeleteMachineAsync(id);
        return RedirectToAction("Index");
    }
}