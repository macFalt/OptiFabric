using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OptiFabricMVC.Application.Interfaces;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Application.Services;
using OptiFabricMVC.Application.ViewModels.JobEmployeeVM;
using OptiFabricMVC.Application.ViewModels.JobVM;
using OptiFabricMVC.Application.ViewModels.OperationVM;
using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;

namespace OptiFabric.Controllers;

public class JobController : Controller
{
    private readonly IJobService _jobService;
    private readonly IProductService _productService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMachineService _machineService;
    private readonly IOperationService _operationService;
    private readonly IJobEmployeeService _jobEmployeeService;
    private readonly IMapper _mapper;

    public JobController(IJobService jobService, IProductService productService,
        UserManager<ApplicationUser> userManager, IMachineService machineService, IOperationService operationService, IJobEmployeeService jobEmployeeService
        , IMapper mapper)
    {
        _jobService = jobService;
        _productService = productService;
        _userManager = userManager;
        _machineService = machineService;
        _operationService = operationService;
        _jobEmployeeService = jobEmployeeService;
        _mapper = mapper;
    }

    //*************************************************
    //****************JobController********************
    //*************************************************
    
    [Authorize]
    public async Task<IActionResult> Index(int pageSize = 10, int pageNo = 1, string searchString = "")
    {
        var model = await _jobService.GetAllJobsAsync(pageSize, pageNo, searchString);
        var userId = _userManager.GetUserId(User);
        ViewBag.CurrentUserId = userId;
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> AddJob(int pageSize = 10, int pageNo = 1, string searchString = "")
    {
        var model = new AddNewJobVM();
        var listProduct = await _productService.GetAllProductsAsync(pageSize, pageNo, searchString);
        model.Products = listProduct.ProductsListVM;
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddJob(AddNewJobVM model)
    {
        if (!ModelState.IsValid)
        {
            var listProduct = await _productService.GetAllProductsAsync(10,1,"");
            model.Products = listProduct.ProductsListVM;

            return View(model);
        }
        var job = await _jobService.AddJob(model);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> EditJob(int id, int pageSize = 10, int pageNo = 1, string searchString = "")
    {
        var job = await _jobService.GetSelectedJobAsync(id);
        var listProduct = await _productService.GetAllProductsAsync(pageSize, pageNo, searchString);
        job.Products = listProduct.ProductsListVM;
        return View(job);
    }

    [HttpPost]
    public async Task<IActionResult> EditJob(EditJobVM model)
    {
        await _jobService.EditJobAsync(model);
        return RedirectToAction("Index");
    }
    
    
    public async Task<IActionResult> ShowOperationList(int JobId,int ProductId  ,int pageSize = 10, int pageNo = 1, string searchString = "")
    {
        var model = _operationService.GetAllOperations(JobId,ProductId, pageSize, pageNo, searchString);
        var emploList = await _jobEmployeeService.GetAllJobEmployeesByJobIdAsync(JobId);
        model.JobEmployees = emploList;
        ViewBag.CurrentUserId = _userManager.GetUserId(User);
        return View(model);
    }
    
    //*********************************************************
    //****************JobEmployeeController********************
    //*********************************************************

    
    [HttpGet]
    public async Task<IActionResult> StartJob(int operationId,int jobId, int pageSize = 10, int pageNo = 1, string searchString = "")
    {
        var model = new MachineSelectionVM();
        var listMachines = await _machineService.GetAllMachines(pageSize, pageNo, searchString);
        model.MachinesForListVms = listMachines.MachinesForListVms;
        model.JobId = jobId;
        model.OperationId = operationId;
        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> StartJob(int operationId, int selectedMachineId, int jobId)
    {

        try
        {
            var data = DateTime.Now;
            var userId = _userManager.GetUserId(User);
            await _jobEmployeeService.StartJobEmployeeAsync(data, userId, operationId, selectedMachineId, jobId);
            return RedirectToAction("ShowOperationList", new { JobId = jobId });
        }
        catch (InvalidOperationException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("StartJob", new { jobId = jobId });
        }
    }

    [HttpGet]
    public IActionResult StopJob(int jobId,int operationId)
    {
        var model = new EndJobEmployeeVM();
        model.JobId = jobId;
        model.OperationId = operationId;
        return View(model);
    }

    [HttpPost]
    public async  Task<IActionResult> StopJob(EndJobEmployeeVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var data = DateTime.Now;
        var userId = _userManager.GetUserId(User);
        model.EndTime = data;
        await _jobEmployeeService.StopJobEmployeeAsync(model, userId);
        return RedirectToAction("ShowOperationList", new { JobId = model.JobId });
    }



    public async Task<IActionResult> ListJobEmployee(int operationId)
    {
        var emploList =await _jobEmployeeService.GetAllJobsEmployeeDetails(operationId);
        
        return View(emploList);
    }

    [HttpGet]
    public async Task<IActionResult> EditJobEmployee(int id)
    {
        var details = await _jobEmployeeService.GetJobEmployeeDetailsAsync(id);
        var model = _mapper.Map<EditJobEmployeeVM>(details);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditJobEmployee(EditJobEmployeeVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await _jobEmployeeService.EditJobEmployee(model);
        return RedirectToAction("ShowOperationList", new { JobId = model.JobId });
    }



}

















 
// [HttpGet]
// public IActionResult StartJob(int JobId, int pageSize = 10, int pageNo = 1, string searchString = "")
// {
//     var model = new MachineSelectionVM();
//     var listMachines = _machineService.GetAllMachines(pageSize, pageNo, searchString);
//     model.MachinesForListVms = listMachines.MachinesForListVms;
//     model.JobId = JobId;
//     return View(model);
// }
//
// [HttpGet]
// public IActionResult AddOperation(int jobId)
// {
//     var model = new OperationForListVM();
//     model.JobId = jobId;
//     return View(model);
// }
//
// [HttpPost]
// public IActionResult AddOperation(OperationForListVM model)
// {
//     _operationService.AddNewOperation(model);
//     return RedirectToAction("ShowOperationList", new { JobId = model.JobId });
// }

// [HttpGet]
// public IActionResult EditOperation(int id)
// {
//     var model = _operationService.GetOperationById(id);
//     return View(model);
// }
//
// [HttpPost]
// public IActionResult EditOperation(OperationForListVM model)
// {
//     _operationService.EditOperation(model);
//     return RedirectToAction("ShowOperationList", new { JobId = model.JobId });
// }