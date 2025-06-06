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
    
    
    public async Task<IActionResult> Index(string sortOrder,int pageSize = 10, int pageNo = 1, string searchString = "")
    {
        var model = await _productService.GetAllProductsAsync(pageSize, pageNo, searchString,sortOrder);
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
        if (!ModelState.IsValid)
        {
            return View(model);
        }

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
        return View(await _productService.GetEditDetailsAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditProductVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
       await _productService.EditProductAsync(model);
        return RedirectToAction("Index");
    }


    
}





















