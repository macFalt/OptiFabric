namespace OptiFabricMVC.Domain.Model;

public class Machine
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public MachineStatus Status { get; set; }

    public ICollection<JobEmployee> JobEmployees { get; set; }

}
public enum MachineStatus
{
    Wolna,
    Zajęta
}