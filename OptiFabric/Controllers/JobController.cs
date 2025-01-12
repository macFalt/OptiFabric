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

    public JobController(IJobService jobService, IProductService productService,UserManager<ApplicationUser> userManager)
    {
        _jobService = jobService;
        _productService = productService;
        _userManager = userManager;
    }

    public IActionResult Index(int pageSize = 10, int pageNo = 1, string searchString = "")
    {
        var model = _jobService.GetAllJobs(pageSize, pageNo, searchString);
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
        model.Product = _productService.GetDetail(model.SelectedProductId);
        var job = _jobService.AddJob(model);
        return RedirectToAction("Index");
    }

    public IActionResult StartJob(int id)
    {
        var data = DateTime.Now;
        var userId = _userManager.GetUserId(User);
        _jobService.StartJobEmployee(data, userId,id);
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
        _jobService.StopJobEmployee(model,data, userId, id);
        return RedirectToAction("Index");
    }
}