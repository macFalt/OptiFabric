using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OptiFabricMVC.Application.Interfaces;
using OptiFabricMVC.Application.ViewModels.ProductsVM;
using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.Services;

public class ProductService: IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository,IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    
    
    
    public async Task<int> AddNewProductAsync(AddNewProductVM model)
    {
        var product = _mapper.Map<Product>(model);
        var id = await _productRepository.AddAsync(product);
        return id;
    }

    public async Task<ProductDetailsVM> GetDetailAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return _mapper.Map<ProductDetailsVM>(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _productRepository.DeleteAsync(id);
    }

    public async Task EditProductAsync(EditProductVM model)
    {
        var product=_mapper.Map<Product>(model);
        await _productRepository.UpdateAsync(product);
    }

    public async Task<ListProductVM> GetAllProductsAsync(int pageSize, int pageNo, string searchString)
    {
        var query=_productRepository.GetAll()
            .Where(p=>p.Name.StartsWith(searchString));
        
        var count = await query.CountAsync();
        
        var productToShow = await query
            .OrderBy(m => m.Name)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .ProjectTo<ProductForListVM>(_mapper.ConfigurationProvider)
            .ToListAsync();

        var productList = new ListProductVM()
        {
            PageSize = pageSize,
            CurrentPage = pageNo,
            SearchString = searchString,
            ProductsListVM = productToShow,
            Count = count
        };

        return productList;
        
    }
    

}
















    
    
    
    
    
// public ProductDetailsVM GetDetail(int id)
// {
//     var product = _productRepository.GetProductFromDB(id);
//     var productVM = _mapper.Map<ProductDetailsVM>(product);
//     return productVM;
// }

// public void DeleteProduct(int id)
// {
//     _productRepository.DeleteProductFromDB(id);
// }

// public void EditProduct(EditProductVM model)
// {
//     var product = _mapper.Map<Product>(model);
//     _productRepository.EditProductDB(product);
// }
    
    
// public ListProductVM GetAllProducts(int pageSize, int pageNo, string searchString)
// {
//     var productList = _productRepository.GetAllProductFromDB().Where(m => m.DrawingNumber.StartsWith(searchString))
//         .ProjectTo<ProductForListVM>(_mapper.ConfigurationProvider).ToList();
//     var productsToShow = productList.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();
//
//     var products = new ListProductVM()
//     {
//         PageSize = pageSize,
//         CurrentPage = pageNo,
//         SearchString = searchString,
//         ProductsListVM = productsToShow,
//         Count = productsToShow.Count
//     };
//
//     return products;
// }