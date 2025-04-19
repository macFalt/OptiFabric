using OptiFabricMVC.Application.ViewModels.ProductsVM;

namespace OptiFabricMVC.Application.Interfaces;

public interface IProductService
{
    Task<int> AddNewProductAsync(AddNewProductVM model);
    Task<ProductDetailsVM> GetDetailAsync(int id);
    Task<EditProductVM> GetEditDetailsAsync(int id);
    Task DeleteProductAsync(int id);
    Task EditProductAsync(EditProductVM model);
    Task<ListProductVM> GetAllProductsAsync(int pageSize, int pageNo, string searchString);



}

















// int AddNewProduct(AddNewProductVM model);
// ListProductVM GetAllProducts(int pageSize, int pageNo, string searchString);
// ProductDetailsVM GetDetail(int id);
// void DeleteProduct(int id);
// void EditProduct(EditProductVM model);