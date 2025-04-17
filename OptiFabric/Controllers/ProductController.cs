using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OptiFabricMVC.Application.Interfaces;
using OptiFabricMVC.Application.ViewModels.OperationVM;
using OptiFabricMVC.Application.ViewModels.ProductsVM;
using OptiFabricMVC.Domain.Model;

namespace OptiFabric.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly IOperationService _operationService;
    private readonly UserManager<ApplicationUser> _userManager;

    

    public ProductController(IProductService productService,IOperationService operationService, UserManager<ApplicationUser> userManager)
    {
        _productService = productService;
        _operationService = operationService;
        _userManager =userManager;
    }
    
    
    public async Task<IActionResult> Index(int pageSize = 10, int pageNo = 1, string searchString = "")
    {
        var model = await _productService.GetAllProductsAsync(pageSize, pageNo, searchString);
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> AddProduct()
    {
        var model = new AddNewProductVM();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(AddNewProductVM model)
    {
        await _productService.AddNewProductAsync(model);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(int id)
    {
        return View(await _productService.GetDetailAsync(id));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteProductAsync(id);
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        return View(await _productService.GetDetailAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditProductVM model)
    {
       await _productService.EditProductAsync(model);
        return RedirectToAction("Index");
    }

    public  async Task<IActionResult> ShowOperationList(int productId, int pageSize = 10, int pageNo = 1, string searchString = "")
    {
        var model = await _operationService.GetAllOperationsPattern(productId, pageSize, pageNo, searchString);
        model.ProductId =productId;
        var userId = _userManager.GetUserId(User);
        ViewBag.CurrentUserId = userId;
        return View(model);
    }
    
}






















// [HttpGet]
//     public IActionResult AddOperationPattern(int productId)
//     {
//         var model = new OperationPatternForListVM();
//         model.ProductId = productId;
//         return View(model);
//     }

// [HttpPost]
// public async Task<IActionResult> AddOperationPattern(OperationPatternForListVM model)
// {
//     await _operationService.AddNewOperationPatternAsync(model);
//     return RedirectToAction("ShowOperationList", new { ProductId = model.ProductId });
// }
//
//
// [HttpGet]
// public async Task<IActionResult> EditOperation(int id)
// {
//     var model = await _operationService.GetOperationPatternAsync(id);
//     return View(model);
// }
//
// [HttpPost]
// public async Task<IActionResult> EditOperation(OperationPatternForListVM model)
// {
//     await _operationService.EditOperationAsync(model);
//     return RedirectToAction("ShowOperationList", new { JobId = model.JobId });
// }