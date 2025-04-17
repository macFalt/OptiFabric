using OptiFabricMVC.Domain.Interfaces;

namespace OptiFabricMVC.Domain.Model;

public class Product : IEntity<int>
{
    public int Id  { get; set; }
    public string Name { get; set; }
    public string DrawingNumber { get; set; }
    
    public string Material { get; set; }
    
    public ICollection<Job> Jobs { get; set; }

    public ICollection<OperationPattern> OperationPattern { get; set; }
    
    

}