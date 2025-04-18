namespace OptiFabricMVC.Application.ViewModels.ProductsVM;

public abstract class  BaseProductVM
{
    public int Id  { get; set; }

    public  string Name { get; set; }

    public  string DrawingNumber { get; set; }

    public  string Material { get; set; }
}