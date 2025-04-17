namespace OptiFabricMVC.Application.ViewModels.JobEmployeeVM;

public class ListJobEmployeeVM
{
    public List<JobEmployeeForListVM> JobEmployeeForListVms { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public string SearchString { get; set; }
    public int Count { get; set; }
}