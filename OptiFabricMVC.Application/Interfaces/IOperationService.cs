using OptiFabricMVC.Application.ViewModels.OperationVM;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.Interfaces;

public interface IOperationService
{
    Task<int> AddNewOperationPatternAsync(OperationPatternForListVM model);
    Task EditOperationAsync(OperationPatternForListVM model);
    Task DeleteOperationAsync(int id);
    Task<OperationPatternForListVM> GetOperationPatternAsync(int id);
    Task<ListOperationPatternVM> GetAllOperationsPattern(int productId, int pageSize, int pageNo, string searchString);
     ListOperationVM GetAllOperations(int jobId,int ProductId, int pageSize, int pageNo, string searchString);
     
     
     
     
     
     
    // OperationForListVM GetOperationById(int id);
    // void EditOperation(OperationForListVM model);
    //ListOperationPatternVM GetAllOperationsPattern(int productId, int pageSize, int pageNo, string searchString);
    //void AddNewOperationPattern(OperationPatternForListVM model);
    // Task<ListOperationPatternVM> GetAllOperationPatternAsync(int ProductId, int pageSize,
    //     int pageNo, string searchString);
}