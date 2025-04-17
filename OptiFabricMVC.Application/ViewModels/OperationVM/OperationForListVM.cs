using AutoMapper;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Application.ViewModels.ProductsVM;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.OperationVM;

public class OperationForListVM : IMapFrom<Operation>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string?  Description { get; set; }
    
    public TimeSpan EstimatedTimePerUnit { get; set; } 
    
    public string? Comments { get; set; } 

    public int RequiredQuantity { get; set; }
    
    public int CompletedQuantity { get; set; } 
        
    public int MissingQuantity { get; set; }

    public int JobId { get; set; }

    public int OperationPatternId { get; set; }

    public OperationStatus OperationStatus { get; set; }
    
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<Operation, OperationForListVM>()
            .ReverseMap();    }
    
    
}


