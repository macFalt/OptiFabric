using AutoMapper;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Application.ViewModels.MachinesVM;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.JobVM;

public class JobsForListVM: IMapFrom<Job>
{
    public int Id { get; set; } // Unikalny identyfikator zadania
    
    public string Description { get; set; } // Opis zadania
    
    public JobStatus JobStatus { get; set; } // Status wykonania zadania
        
    public int RequiredQuantity { get; set; } // Ilość sztuk potrzebnych
    
    public int? TotalCompletedQuantity { get; set; } // Ilość wykonanych sztuk
        
    public int? TotalMissingQuantity { get; set; } // Ilość braków
    
    public DateTime CreatedAt { get; set; } = DateTime.Now; // Data utworzenia zadania
    public DateTime? CompletedAt { get; set; } // Data ukończenia zadania (opcjonalna)
    
    public int ProductId { get; set; } 
    public Product Product { get; set; } // Produkt, do którego przypisane jest zadanie
    
    
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<Job, JobsForListVM>().ReverseMap();
    }
}

