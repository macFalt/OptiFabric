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

public class MachinesForListVMValidator: BaseMachineValidator<MachinesForListVM>{}

public enum MachineStatus
{
    Wolna,
    Zajęta,
    Uszkodzona
}