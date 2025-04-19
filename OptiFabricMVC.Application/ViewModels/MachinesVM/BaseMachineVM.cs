namespace OptiFabricMVC.Application.ViewModels.MachinesVM;

public abstract class BaseMachineVM
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public MachineStatus Status { get; set; }
}