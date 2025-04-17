using OptiFabricMVC.Application.ViewModels.MachinesVM;

namespace OptiFabricMVC.Application.ViewModels.JobVM;

public class MachineSelectionVM 
{
    public List<MachinesForListVM> MachinesForListVms { get; set; }
    public int JobId { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public string SearchString { get; set; }
    public int Count { get; set; }
}