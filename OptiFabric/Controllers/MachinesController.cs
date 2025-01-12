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
    public IActionResult Index(int pageSize = 10, int pageNo = 1, string searchString = "")
    {
        var model = _machineService.GetAllMachines(pageSize,pageNo,searchString);
        return View(model);
    }

    [HttpGet]
    public IActionResult AddMachine()
    {
        var model = new MachinesForListVM();
        return View(model);
    }

    [HttpPost]
    public IActionResult AddMachine(MachinesForListVM model)
    {
        var machine = _machineService.AddMachine(model);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult EditMachine(int id)
    {
        var model = _machineService.GetDetails(id);
        return View(model);
    }

    [HttpPost]
    public IActionResult EditMachine(EditMachineVM model)
    {
        _machineService.EditMachine(model);
        return RedirectToAction("Index");
    }

    public IActionResult Details(int id)
    {
        var machines = _machineService.GetDetails(id);
        return View(machines);
    }

    public IActionResult Delete(int id)
    {
        _machineService.DeleteMachine(id);
        return RedirectToAction("Index");
    }
}