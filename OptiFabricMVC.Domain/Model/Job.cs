using OptiFabricMVC.Domain.Interfaces;

namespace OptiFabricMVC.Domain.Model;

public class Job : IEntity<int>
{
    public int Id { get; set; } 
    
    public string Description { get; set; } 
    public JobStatus JobStatus { get; set; } 
    public bool ActivEmployeeJob { get; set; }
    public int RequiredQuantity { get; set; } 
    public int? TotalCompletedQuantity { get; set; } 
        
    public int? TotalMissingQuantity { get; set; } 
    
    public DateTime CreatedAt { get; set; } = DateTime.Now; 
    public DateTime? CompletedAt { get; set; } 
    
    public int ProductId { get; set; } 
    public Product Product { get; set; }

    public ICollection<Operation> Operations { get; set; }
    
}

public enum JobStatus
{
InProgress,
Completed,
NotStarted,
Cancelled
}