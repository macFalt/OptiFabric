namespace OptiFabricMVC.Application.ViewModels.EmployeeVM;

public class ListEmployeeVM
{
    public List<EmployeeForListVM> EmployeeForListVms { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public string SearchString { get; set; }
    public int Count { get; set; }
    
    public List<string> AvailableRoles { get; set; } 

}