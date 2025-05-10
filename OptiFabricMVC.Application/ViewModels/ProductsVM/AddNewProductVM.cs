using AutoMapper;
using FluentValidation;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Application.ViewModels.EmployeeVM;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.ProductsVM;

public class AddNewProductVM: BaseProductVM, IMapFrom<Product>
{
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<Product, AddNewProductVM>().ReverseMap();
    }
}



















