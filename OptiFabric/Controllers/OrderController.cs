using Microsoft.AspNetCore.Mvc;
using OptiFabricMVC.Application.Interfaces;
using OptiFabricMVC.Application.ViewModels.OrdersVM;

namespace OptiFabric.Controllers;

public class OrderController : Controller
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    // GET
    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public IActionResult AddOrder()
    {
        var model = new AddNewOrderVM();
        return View(model);
    }

    [HttpPost]
    public IActionResult AddOrder(AddNewOrderVM model)
    {
        var order = _orderService.AddOrder(model);
        return RedirectToAction("Index");
    }
}