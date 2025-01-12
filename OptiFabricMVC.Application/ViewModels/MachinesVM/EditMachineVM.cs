using AutoMapper;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.MachinesVM;

public class EditMachineVM: IMapFrom<Machine>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public Domain.Model.MachineStatus Status { get; set; }

  
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<Machine, EditMachineVM>().ReverseMap();
    }
}



