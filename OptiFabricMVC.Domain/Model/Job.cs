namespace OptiFabricMVC.Domain.Model;

public class Job
{
    public int Id { get; set; } // Unikalny identyfikator zadania
        
    public string Description { get; set; } // Opis zadania
    
    public JobStatus IsCompleted { get; set; } // Status wykonania zadania

    public bool ActivEmployeeJob { get; set; }
        
    public int RequiredQuantity { get; set; } // Ilość sztuk potrzebnych
    
    public int? TotalCompletedQuantity { get; set; } // Ilość wykonanych sztuk
        
    public int? TotalMissingQuantity { get; set; } // Ilość braków
    
    public DateTime CreatedAt { get; set; } = DateTime.Now; // Data utworzenia zadania
    public DateTime? CompletedAt { get; set; } // Data ukończenia zadania (opcjonalna)
    
    public int ProductId { get; set; } // Klucz obcy do produktu
    public Product Product { get; set; } // Produkt, do którego przypisane jest zadanie

    public ICollection<Operation> Operations { get; set; }
    public ICollection<JobEmployee> JobEmployees { get; set; }
        

    
    
}

public enum JobStatus
{
InProgress,
Completed,
NotStarted
}