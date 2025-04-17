using System.ComponentModel.DataAnnotations;
using AutoMapper;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Application.ViewModels.ProductsVM;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.JobVM;

public class AddNewJobVM: IMapFrom<Job>
{
    public int Id { get; set; } 
        
    public string Description { get; set; } 
    public JobStatus JobStatus { get; set; } 
    
    
    public int RequiredQuantity { get; set; } 

    public int TotalCompletedQuantity { get; set; } 

    public int TotalMissingQuantity { get; set; } 
    
    public DateTime CreatedAt { get; set; } = DateTime.Now; 
    public DateTime? CompletedAt { get; set; } 

    public List<ProductForListVM> Products { get; set; }
    
    public ProductDetailsVM Product { get; set; }

    public int ProductId { get; set; }    
    
    
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<Job, AddNewJobVM>().ReverseMap();
    }
}
