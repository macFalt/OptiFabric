namespace OptiFabricMVC.Application.ViewModels.MachinesVM;

public class ListMachinesVM
{
    public List<MachinesForListVM> MachinesForListVms { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public string SearchString { get; set; }
    public int Count { get; set; }
}