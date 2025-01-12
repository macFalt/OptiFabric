namespace OptiFabricMVC.Domain.Model;

public class Operation
{
    public int Id { get; set; }

    public string Name { get; set; }
    
    public TimeSpan EstimatedTimePerUnit { get; set; } // Przewidywany czas na wykonanie jednej sztuki dla tej operacji

    public ICollection<Job> Jobs { get; set; }
    
}