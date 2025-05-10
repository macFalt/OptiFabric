using AutoMapper;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Application.ViewModels.EmployeeVM;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.MachinesVM;

public class MachinesForListVM: BaseMachineVM, IMapFrom<Machine>
{
    
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<Machine, MachinesForListVM>().ReverseMap();
    }
}


public enum MachineStatus
{
    Wolna=1,
    Zajęta=2,
    Uszkodzona=3
}