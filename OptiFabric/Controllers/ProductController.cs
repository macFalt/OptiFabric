using Microsoft.AspNetCore.Mvc;
using OptiFabricMVC.Application.Interfaces;
using OptiFabricMVC.Application.ViewModels.ProductsVM;

namespace OptiFabric.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    
    public IActionResult Index(int pageSize = 10, int pageNo = 1, string searchString = "")
    {
        //problem z dodaniem produktu z wzgledu na brak wszytskich danych
        var model = _productService.GetAllProducts(pageSize, pageNo, searchString);
        return View(model);
    }

    [HttpGet]
    public IActionResult AddProduct()
    {
        var model = new AddNewProductVM();
        return View(model);
    }

    [HttpPost]
    public IActionResult AddProduct(AddNewProductVM model)
    {
        var product=_productService.AddNewProduct(model);
        return RedirectToAction("Index");
    }

    public IActionResult Details(int id)
    {
        var model = _productService.GetDetail(id);
        return View(model);
    }

    public IActionResult Delete(int id)
    {
        _productService.DeleteProduct(id);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var model = _productService.GetDetail(id);
        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(EditProductVM model)
    {
        _productService.EditProduct(model);
        return RedirectToAction("Index");
    }
}