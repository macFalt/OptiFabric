using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Infrastructure.Repositories;

public class ProductRepository: IProductRepository
{
    private readonly Context _context;

    public ProductRepository(Context context)
    {
        _context = context;
    }
    public int AddProductToDB(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return product.Id;
    }

    public IQueryable<Product> GetAllProductFromDB()
    {
        return _context.Products.AsQueryable();
    }

    public Product GetProductFromDB(int id)
    {
        return _context.Products.FirstOrDefault(p => p.Id == id);
    }

    public void DeleteProductFromDB(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        _context.Products.Remove(product);
        _context.SaveChanges();
    }

    public void EditProductDB(Product product)
    {
        var prod = _context.Products.FirstOrDefault(p => p.Id == product.Id);
        prod.Name = product.Name;
        prod.DrawingNumber = product.DrawingNumber;
        prod.Material = product.Material;
        _context.SaveChanges();
    }
}