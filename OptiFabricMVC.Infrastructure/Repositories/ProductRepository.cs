using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Infrastructure.Repositories;

public class ProductRepository: GenericRepository<Product,int> , IProductRepository
{
    private readonly Context _context;

    public ProductRepository(Context context) : base(context)
    {
        _context = context;
    }
    
    
}


