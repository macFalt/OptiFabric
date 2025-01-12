using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public int AddNewProduct(AddNewProductVM model)
    {
        var product = _mapper.Map<Product>(model);
        var id = _productRepository.AddProductToDB(product);
        return id;
    }

    public ListProductVM GetAllProducts(int pageSize, int pageNo, string searchString)
    {
        var productList = _productRepository.GetAllProductFromDB().Where(m => m.DrawingNumber.StartsWith(searchString))
            .ProjectTo<ProductForListVM>(_mapper.ConfigurationProvider).ToList();
        var productsToShow = productList.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();

        var products = new ListProductVM()
        {
            PageSize = pageSize,
            CurrentPage = pageNo,
            SearchString = searchString,
            ProductsListVM = productsToShow,
            Count = productsToShow.Count
        };

        return products;
    }

    public ProductDetailsVM GetDetail(int id)
    {
        var product = _productRepository.GetProductFromDB(id);
        var productVM = _mapper.Map<ProductDetailsVM>(product);
        return productVM;
    }

    public void DeleteProduct(int id)
    {
        _productRepository.DeleteProductFromDB(id);
    }

    public void EditProduct(EditProductVM model)
    {
        var product = _mapper.Map<Product>(model);
        _productRepository.EditProductDB(product);
    }
}