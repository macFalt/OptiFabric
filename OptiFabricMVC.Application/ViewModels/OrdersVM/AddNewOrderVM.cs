using AutoMapper;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Application.ViewModels.MachinesVM;
using OptiFabricMVC.Application.ViewModels.ProductsVM;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.OrdersVM;

public class AddNewOrderVM: IMapFrom<Order>
{
    public int Id { get; set; } // Unikalny identyfikator zamówienia
        
    public DateTime OrderDate { get; set; } // Data złożenia zamówienia
    
    public DateTime DeadLine { get; set; } // Data realizacji zamówienia
        
    public string CompanyName { get; set; } // Nazwa firmy, z której pochodzi zlecenie
    
    public decimal TotalAmount { get; set; } // Łączna kwota zamówienia
    
    public OrderStatus Status { get; set; }

    public List<ProductForListVM> Products { get; set; } = new List<ProductForListVM>();


    
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<Order, AddNewOrderVM>().ReverseMap();
    }
    
}
public enum OrderStatus
{
    Placed,
    InProgress,
    Completed,
    Cancelled
}
