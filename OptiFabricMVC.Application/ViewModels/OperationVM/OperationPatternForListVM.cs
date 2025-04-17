using AutoMapper;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.OperationVM;

public class OperationPatternForListVM : IMapFrom<OperationPattern>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string?  Description { get; set; }
    
    public TimeSpan EstimatedTimePerUnit { get; set; } 

    public int ProductId { get; set; }
    
    public int JobId { get; set; }

    
    
    public void ConfigureMapping(Profile profile)
    {
        profile.CreateMap<OperationPattern, OperationPatternForListVM>()
            .ReverseMap();    }
    
    
}
