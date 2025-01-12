using AutoMapper;
using OptiFabricMVC.Application.Interfaces;
using OptiFabricMVC.Application.ViewModels.OrdersVM;
using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.Services;

public class OrderService: IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    public int AddOrder(AddNewOrderVM model)
    {
        var order = _mapper.Map<Order>(model);
        var id = _orderRepository.AddOrderToDB(order);
        return id;

    }
}