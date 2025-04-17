namespace OptiFabricMVC.Application.ViewModels.OperationVM;

public class ListOperationPatternVM
{
    public List<OperationPatternForListVM> OperationPatternForListVms { get; set; }
    
    public int ProductId { get; set; }

    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public string SearchString { get; set; }
    public int Count { get; set; }
}