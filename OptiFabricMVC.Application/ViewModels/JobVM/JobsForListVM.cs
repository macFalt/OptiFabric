using AutoMapper;
using FluentValidation;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Application.ViewModels.MachinesVM;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.JobVM;

public class JobsForListVM: BaseJobVM,  IMapFrom<Job>
{

     public Product Product { get; set; }
    
    
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<Job, JobsForListVM>().ReverseMap();
    }
    
}

public class JobForEditValidation : BaseJobValidator<JobsForListVM>
{
    public JobForEditValidation()
    {
        RuleFor(x => x.ProductId).NotEmpty();
    }
}













// public int Id { get; set; } // Unikalny identyfikator zadania
// public string Description { get; set; } // Opis zadania
// public JobStatus JobStatus { get; set; } // Status wykonania zadania
// public int RequiredQuantity { get; set; } // Ilość sztuk potrzebnych
// public int? TotalCompletedQuantity { get; set; } // Ilość wykonanych sztuk
// public int? TotalMissingQuantity { get; set; } // Ilość braków
// public DateTime CreatedAt { get; set; } = DateTime.Now; // Data utworzenia zadania
// public DateTime? CompletedAt { get; set; } // Data ukończenia zadania (opcjonalna)
//public int ProductId { get; set; } 
