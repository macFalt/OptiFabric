using AutoMapper;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Application.ViewModels.EmployeeVM;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.MachinesVM;

public class MachinesForListVM: IMapFrom<Machine>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public MachineStatus Status { get; set; }

  
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<Machine, MachinesForListVM>().ReverseMap();
    }
}


public enum MachineStatus
{
    Wolna,
    Zajęta,
    Uszkodzona
}