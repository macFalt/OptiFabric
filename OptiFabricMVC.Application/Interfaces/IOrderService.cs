using OptiFabricMVC.Application.ViewModels.OrdersVM;

namespace OptiFabricMVC.Application.Interfaces;

public interface IOrderService
{
    int AddOrder(AddNewOrderVM model);

    // object GetAllOrders(int jobId);
}