namespace OptiFabricMVC.Domain.Model;

public class Order
{
    public int Id { get; set; } // Unikalny identyfikator zamówienia
        
    public DateTime OrderDate { get; set; } // Data złożenia zamówienia
    
    public DateTime DeadLine { get; set; } // Data realizacji zamówienia
        
    public string CompanyName { get; set; } // Nazwa firmy, z której pochodzi zlecenie
    
    public decimal TotalAmount { get; set; } // Łączna kwota zamówienia
    
    public OrderStatus Status { get; set; }
    
    public ICollection<Product> Products { get; set; }

}
public enum OrderStatus
{
    Placed,
    InProgress,
    Completed,
    Cancelled
}