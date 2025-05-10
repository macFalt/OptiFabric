using AutoMapper;
using FluentValidation;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.ProductsVM;

public class EditProductVM: BaseProductVM, IMapFrom<Product>
{
    
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<Product, EditProductVM>().ReverseMap();
    }
}

