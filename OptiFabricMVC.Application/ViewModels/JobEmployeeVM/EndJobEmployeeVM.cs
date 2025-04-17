using AutoMapper;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.JobEmployeeVM;

public class EndJobEmployeeVM : IMapFrom<JobEmployee>
{
    public int Id { get; set; }
    
    public string? EmployeeComments { get; set; } // Uwagi pracownika
    
    public int CompletedQuantity { get; set; } // Ilość wykonanych sztuk
        
    public int MissingQuantity { get; set; } // Ilość braków
    
    public DateTime StartTime { get; set; } 
    
    public DateTime EndTime { get; set; }

    public double WorkTime => (EndTime - StartTime).TotalMinutes;

    public int JobId { get; set; }
    
    // public string CurrentWorkerId { get; set; } // ID aktualnego pracownika
    // public ApplicationUser CurrentWorker { get; set; } // Referencja do pracownika
    //
    // public int JobId { get; set; }
    // public Job Job { get; set; }
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<JobEmployee, EndJobEmployeeVM>().ReverseMap();
    }
}