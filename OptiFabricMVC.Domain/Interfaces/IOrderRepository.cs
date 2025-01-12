using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Domain.Interfaces;

public interface IOrderRepository
{
    int AddOrderToDB(Order order);
}