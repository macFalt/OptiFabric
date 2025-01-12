using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Infrastructure.Repositories;

public class OrderRepository: IOrderRepository
{
    private readonly Context _context;

    public OrderRepository(Context context)
    {
        _context = context;
    }
    public int AddOrderToDB(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
        return order.Id;
    }
}