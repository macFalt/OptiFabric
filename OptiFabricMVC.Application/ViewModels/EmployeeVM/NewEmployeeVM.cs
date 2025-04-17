using AutoMapper;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.EmployeeVM;

public class NewEmployeeVM: IMapFrom<ApplicationUser>
{
    //public string Id { get; set; }
    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Position { get; set; }

    public string? Password { get; set; }

    public string? NrLogin { get; set; }

    public string SelectedRole { get; set; } 
    public List<string> AvailableRoles { get; set; } 

    
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<ApplicationUser, NewEmployeeVM>().ReverseMap()
            .ForMember(dest => dest.NrLogin, opt => opt.MapFrom(src => src.NrLogin))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Name+src.Surname))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.NrLogin+"@user.pl"))
            .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => true))
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            ;
    }
    
}