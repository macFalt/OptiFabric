using AutoMapper;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.JobEmployeeVM;

public class EditJobEmployeeVM : IMapFrom<JobEmployee>
{
    public int Id { get; set; }

    public string EmployeeName { get; set; }

    public string EmployeeSurname { get; set; }

    public string FullName => $"{EmployeeName} {EmployeeSurname}".Trim();
    
    public string? EmployeeComments { get; set; } // Uwagi pracownika
    
    public int CompletedQuantity { get; set; } // Ilość wykonanych sztuk
    
        
    public int MissingQuantity { get; set; } // Ilość braków
    
    public DateTime StartTime { get; set; } 
    
    public DateTime EndTime { get; set; }
    
    public string CurrentWorkerId { get; set; } // ID aktualnego pracownika

    public int JobId { get; set; }
    public int MachineId { get; set; }
    
    public TimeSpan EstimatedTimePerUnit { get; set; }

    public double Efficiency { get; set; }


    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<JobEmployee, EditJobEmployeeVM>().ReverseMap();
        profile.CreateMap<ApplicationUser, DetailsJobEmployeeVM>().ReverseMap();
        profile.CreateMap<Operation, DetailsJobEmployeeVM>()
            .ForMember(dest => dest.EstimatedTimePerUnit, opt => opt.MapFrom(src => src.EstimatedTimePerUnit));


    }
}