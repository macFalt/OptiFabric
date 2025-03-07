using AutoMapper;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.ProductsVM;

public class ProductForListVM: IMapFrom<Product>
{
    public int Id  { get; set; }

    public string Name { get; set; }

    public string DrawingNumber { get; set; }

    public string Material { get; set; }
    
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<Product, ProductForListVM>().ReverseMap();
    }
}