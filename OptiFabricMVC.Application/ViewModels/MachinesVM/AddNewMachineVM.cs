using AutoMapper;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Application.ViewModels.ProductsVM;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.MachinesVM;

public class AddNewMachineVM: BaseMachineVM, IMapFrom<Machine>
{
    
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<Machine, AddNewMachineVM>().ReverseMap();
    }
}


