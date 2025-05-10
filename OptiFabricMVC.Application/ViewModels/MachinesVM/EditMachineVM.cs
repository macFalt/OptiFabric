using AutoMapper;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.MachinesVM;

public class EditMachineVM: BaseMachineVM, IMapFrom<Machine>
{
    
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<Machine, EditMachineVM>().ReverseMap();
    }
}




