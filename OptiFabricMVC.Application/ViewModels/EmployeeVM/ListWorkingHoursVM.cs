namespace OptiFabricMVC.Application.ViewModels.EmployeeVM;

public class ListWorkingHoursVM
{
    public List<WorkingHoursVM> WorkingHours { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public string SearchString { get; set; }
    public int Count { get; set; }
}