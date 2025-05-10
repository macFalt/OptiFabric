using AutoMapper;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.ProductsVM;

public class ProductForListVM: BaseProductVM, IMapFrom<Product>
{
    
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<Product, ProductForListVM>().ReverseMap();
    }
}

