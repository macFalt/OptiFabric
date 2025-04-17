using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Domain.Interfaces;

public interface IOperationRepository : IGenericRepository<OperationPattern,int>
{
    IQueryable<OperationPattern> GetAllOperationsPatternFromDB(int ProductId);
    IQueryable<Operation> GetAllOperationsFromDB();
   // Operation GetOperationFromDB(int id);

   Task<Operation> GetOperationFromDB(int id);

}












    
    
    
    
    
    
    
    
// Task<List<OperationPattern>> GetAllOperationsPatternFromDB(int ProductId);
// void EditOperationDB(Operation operation);
// void AddOperatonPatternToDB(OperationPattern operationPattern);
// void AddOperatonToDB(Operation operation);
