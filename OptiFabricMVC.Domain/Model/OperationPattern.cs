using OptiFabricMVC.Domain.Interfaces;

namespace OptiFabricMVC.Domain.Model;

public class OperationPattern : IEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string?  Description { get; set; }
    
    public TimeSpan EstimatedTimePerUnit { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }
    

}