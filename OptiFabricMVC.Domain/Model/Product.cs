namespace OptiFabricMVC.Domain.Model;

public class Product
{
    public int Id  { get; set; }

    public string Name { get; set; }

    public string DrawingNumber { get; set; }

    public string Material { get; set; }
    
    public ICollection<Job> Jobs { get; set; }  

}