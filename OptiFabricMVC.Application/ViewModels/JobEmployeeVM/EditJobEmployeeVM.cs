using AutoMapper;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.JobEmployeeVM;

public class EditJobEmployeeVM : BaseJobEmployeeVM, IMapFrom<JobEmployee>
{
    
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<JobEmployee, EditJobEmployeeVM>().ReverseMap();
        profile.CreateMap<ApplicationUser, DetailsJobEmployeeVM>().ReverseMap();
        profile.CreateMap<EditJobEmployeeVM, DetailsJobEmployeeVM>().ReverseMap();
        profile.CreateMap<Operation, DetailsJobEmployeeVM>()
            .ForMember(dest => dest.EstimatedTimePerUnit, opt => opt.MapFrom(src => src.EstimatedTimePerUnit));
    }
}