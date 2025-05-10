using AutoMapper;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.JobEmployeeVM;

public class JobEmployeeForListVM : BaseJobEmployeeVM, IMapFrom<JobEmployee>
{
    public bool IsActive { get; set; }
    
    
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<JobEmployee, JobEmployeeForListVM>().ReverseMap();
    }
}

