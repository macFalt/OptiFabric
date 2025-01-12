using Microsoft.AspNetCore.Identity;

namespace OptiFabricMVC.Domain.Model;

public class ApplicationUser: IdentityUser
{
    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Position { get; set; }

    public string NrLogin { get; set; }

    //public ICollection<ShiftApplicationUser> Shifts { get; set; }
    
    
    
    
}