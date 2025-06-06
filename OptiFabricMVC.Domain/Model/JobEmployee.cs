using OptiFabricMVC.Domain.Interfaces;

namespace OptiFabricMVC.Domain.Model;

public class JobEmployee : IEntity<int>
{
    public int Id { get; set; }
    
    public string? EmployeeComments { get; set; } // Uwagi pracownika
    
    public int CompletedQuantity { get; set; } // Ilość wykonanych sztuk
        
    public int MissingQuantity { get; set; } // Ilość braków

    public bool IsActive { get; set; }
    
    public DateTime StartTime { get; set; } 
    
    public DateTime EndTime { get; set; }

    public double? WorkTime => (EndTime - StartTime).TotalMinutes;

    public int JobId { get; set; }

    public int MachineId { get; set; }
    public Machine Machine { get; set; }
    
    public string CurrentWorkerId { get; set; } 
    public ApplicationUser CurrentWorker { get; set; } 
    
    public int OperationId { get; set; }
    public Operation Operation { get; set; }
    

}