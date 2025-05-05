using OptiFabricMVC.Application.ViewModels.JobEmployeeVM;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.OperationVM;

public class ListOperationVM
{
    public List<OperationForListVM> OperationForListVms { get; set; }
    
    public List<JobEmployeeForListVM> JobEmployees { get; set; }
    
    public int JobId { get; set; }

    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public string SearchString { get; set; }
    public int Count { get; set; }
}