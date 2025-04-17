using Microsoft.AspNetCore.DataProtection.KeyManagement;
using OptiFabricMVC.Domain.Interfaces;

namespace OptiFabricMVC.Domain.Model;

public class Operation : IEntity<int>
{
    
    public int Id { get; set; }
    public string Name { get; set; }

    public string?  Description { get; set; }
    public TimeSpan EstimatedTimePerUnit { get; set; } 
    
    public string? Comments { get; set; } 

    public int RequiredQuantity { get; set; }
    
    public int CompletedQuantity { get; set; } 
    public int MissingQuantity { get; set; } 
    public OperationStatus OperationStatus { get; set; }
    
    public ICollection<JobEmployee> JobEmployees { get; set; }
    
    public int OperationPatternId { get; set; } 
    public OperationPattern OperationPattern { get; set; }

    public int JobId { get; set; }
    public Job Job { get; set; }
}

public enum OperationStatus
{
    InProgress,
    Completed,
    NotStarted,
    Cancelled
}