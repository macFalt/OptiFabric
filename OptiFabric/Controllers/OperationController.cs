using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OptiFabricMVC.Application.Interfaces;
using OptiFabricMVC.Application.ViewModels.OperationVM;
using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;

namespace OptiFabric.Controllers;

public class OperationController : Controller
{
    private readonly IOperationService _operationService;
    private readonly UserManager<ApplicationUser> _userManager;


    public OperationController(IOperationService operationService, UserManager<ApplicationUser> userManager)
    {
        _operationService = operationService;
        _userManager = userManager;
    }
    
    
    
    [HttpGet]
    public IActionResult AddOperationPattern(int productId)
    {
        var model = new OperationPatternForListVM();
        model.ProductId = productId;
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddOperationPattern(OperationPatternForListVM model)
    {
        await _operationService.AddNewOperationPatternAsync(model);
        return RedirectToAction("ShowOperationList", new { ProductId = model.ProductId });
    }


    [HttpGet]
    public async Task<IActionResult> EditOperation(int id)
    {
        var model = await _operationService.GetOperationPatternAsync(id);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditOperation(OperationPatternForListVM model)
    {
        await _operationService.EditOperationAsync(model);
        return RedirectToAction("ShowOperationList", new { productId = model.ProductId });
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