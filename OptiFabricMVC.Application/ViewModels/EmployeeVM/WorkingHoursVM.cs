using AutoMapper;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.EmployeeVM;

public class WorkingHoursVM: IMapFrom<Shift>
{
    public string UserId { get; set; }
    
    public DateTime StartTime { get; set; } 
    
    public DateTime EndTime { get; set; }
    
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<Shift, WorkingHoursVM>().ReverseMap();
    }
    
}