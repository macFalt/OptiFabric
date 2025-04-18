using AutoMapper;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Application.ViewModels.EmployeeVM;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.ProductsVM;

public class ProductDetailsVM: BaseProductVM, IMapFrom<Product>
{
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<Product, ProductDetailsVM>().ReverseMap();
    }
}

public class ProductDetailsVmValidation: BaseProductValidator<ProductDetailsVM>{}