using OptiFabricMVC.Domain.Interfaces;

namespace OptiFabricMVC.Domain.Model;

public class Machine : IEntity<int>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public MachineStatus Status { get; set; }

    public ICollection<JobEmployee> JobEmployees { get; set; }

}
public enum MachineStatus
{
    Wolna=1,
    Zajęta=2,
    Uszkodzona=3
}