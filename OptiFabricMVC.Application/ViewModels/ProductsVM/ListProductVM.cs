namespace OptiFabricMVC.Application.ViewModels.ProductsVM;

public class ListProductVM
{
    public List<ProductForListVM> ProductsListVM { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public string SearchString { get; set; }
    public int Count { get; set; }
}