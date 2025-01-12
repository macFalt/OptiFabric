using OptiFabricMVC.Application.ViewModels.ProductsVM;

namespace OptiFabricMVC.Application.Interfaces;

public interface IProductService
{
    int AddNewProduct(AddNewProductVM model);
    ListProductVM GetAllProducts(int pageSize, int pageNo, string searchString);
    ProductDetailsVM GetDetail(int id);
    void DeleteProduct(int id);
    void EditProduct(EditProductVM model);
}