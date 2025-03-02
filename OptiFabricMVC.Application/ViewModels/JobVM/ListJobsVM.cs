using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.JobVM;

public class ListJobsVM
{
    public List<JobsForListVM> JobsForList { get; set; }
    
    public List<JobEmployee> JobEmployees { get; set; }
    

    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public string SearchString { get; set; }
    public int Count { get; set; }
    

}