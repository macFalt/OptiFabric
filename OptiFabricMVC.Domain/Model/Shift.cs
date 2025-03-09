namespace OptiFabricMVC.Domain.Model;

public class Shift
{
    public int Id { get; set; }
    
    public DateTime StartTime { get; set; } 
    
    public DateTime? EndTime { get; set; }

    public bool isActive { get; set; }

    public string UserId { get; set; }

    public ApplicationUser ApplicationUser { get; set; }
    
    //public ICollection<ShiftApplicationUser> Shifts { get; set; }

    
}