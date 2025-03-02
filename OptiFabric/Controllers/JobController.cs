using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OptiFabricMVC.Application.Interfaces;
using OptiFabricMVC.Application.ViewModels.JobVM;
using OptiFabricMVC.Domain.Model;

namespace OptiFabric.Controllers;

public class JobController : Controller
{
    private readonly IJobService _jobService;
    private readonly IProductService _productService;
    private readonly UserManager<ApplicationUser> _userManager;

    public JobController(IJobService jobService, IProductService productService,
        UserManager<ApplicationUser> userManager)
    {
        _jobService = jobService;
        _productService = productService;
        _userManager = userManager;
    }

    public IActionResult Index(int pageSize = 10, int pageNo = 1, string searchString = "")
    {
        var model = _jobService.GetAllJobs(pageSize, pageNo, searchString);
        var emploList = _jobService.GetAllJobsEmployee();
        model.JobEmployees = emploList;
        var userId = _userManager.GetUserId(User);
        ViewBag.CurrentUserId = userId;
        return View(model);
    }

    [HttpGet]
    public IActionResult AddJob(int pageSize = 10, int pageNo = 1, string searchString = "")
    {
        var model = new AddNewJobVM();
        var listProduct = _productService.GetAllProducts(pageSize, pageNo, searchString);
        model.Products = listProduct.ProductsListVM;
        return View(model);
    }

    [HttpPost]
    public IActionResult AddJob(AddNewJobVM model)
    {
        model.Product = _productService.GetDetail(model.ProductId);
        var job = _jobService.AddJob(model);
        return RedirectToAction("Index");
    }

    public IActionResult StartJob(int id)
    {
        var data = DateTime.Now;
        var userId = _userManager.GetUserId(User);
        _jobService.StartJobEmployee(data, userId, id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult StopJob()
    {
        var model = new EndJobEmployeeVM();
        return View(model);
    }

    [HttpPost]
    public IActionResult StopJob(EndJobEmployeeVM model, int id)
    {
        var data = DateTime.Now;
        var userId = _userManager.GetUserId(User);
        model.EndTime = data;
        _jobService.StopJobEmployee(model, data, userId, id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult EditJob(int id, int pageSize = 10, int pageNo = 1, string searchString = "")
    {
        var job = _jobService.GetSelectedJob(id);
        var listProduct = _productService.GetAllProducts(pageSize, pageNo, searchString);
        job.Products = listProduct.ProductsListVM;
        return View(job);
    }

    [HttpPost]
    public IActionResult EditJob(AddNewJobVM model)
    {
        _jobService.EditJob(model);
        return RedirectToAction("Index");
    }

    public IActionResult ListJobEmployee(int id)
    {
        var emploList = _jobService.GetAllJobsEmployeeDetails(id);
        return View(emploList);
    }

    [HttpGet]
    public IActionResult EditJobEmployee(int id)
    {
        var model = _jobService.GetJobEmployeeDetailById(id);
        return View(model);
    }

    [HttpPost]
    public IActionResult EditJobEmployee(DetailsJobEmployeeVM model)
    {
        _jobService.EditJobEmployee(model);
        return RedirectToAction("Index");
    }
}