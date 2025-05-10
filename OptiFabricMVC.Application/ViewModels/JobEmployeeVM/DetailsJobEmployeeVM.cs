using AutoMapper;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.JobEmployeeVM;

public class DetailsJobEmployeeVM : BaseJobEmployeeVM, IMapFrom<JobEmployee>
{
    
    public string EmployeeName { get; set; }

    public string EmployeeSurname { get; set; }

    public string FullName => $"{EmployeeName} {EmployeeSurname}".Trim();
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<JobEmployee, DetailsJobEmployeeVM>().ReverseMap();
        profile.CreateMap<Operation, DetailsJobEmployeeVM>()
            .ForMember(dest => dest.EstimatedTimePerUnit, opt => opt.MapFrom(src => src.EstimatedTimePerUnit));


    }
}