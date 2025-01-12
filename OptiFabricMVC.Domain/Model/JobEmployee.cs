namespace OptiFabricMVC.Domain.Model;

public class JobEmployee
{
    public int Id { get; set; }
    
    public string? EmployeeComments { get; set; } // Uwagi pracownika
    
    public int CompletedQuantity { get; set; } // Ilość wykonanych sztuk
        
    public int MissingQuantity { get; set; } // Ilość braków
    
    public DateTime StartTime { get; set; } 
    
    public DateTime EndTime { get; set; }

    public double WorkTime => (EndTime - StartTime).TotalMinutes;
    
    public string CurrentWorkerId { get; set; } // ID aktualnego pracownika
    public ApplicationUser CurrentWorker { get; set; } // Referencja do pracownika

    public int JobId { get; set; }
    public Job Job { get; set; }

}