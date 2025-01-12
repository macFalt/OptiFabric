using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Domain.Interfaces;

public interface IProductRepository
{
    int AddProductToDB(Product product);
    IQueryable<Product> GetAllProductFromDB();
    Product GetProductFromDB(int id);
    void DeleteProductFromDB(int id);
    void EditProductDB(Product product);
}