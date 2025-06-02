using Microsoft.EntityFrameworkCore;
using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Infrastructure.Repositories;

public class OperationRepository : GenericRepository<OperationPattern,int>, IOperationRepository
{
    private readonly Context _context;

    public OperationRepository(Context context) : base(context)
    {
        _context = context;
    }

    
    public IQueryable<OperationPattern> GetAllOperationsPatternFromDB(int ProductId)
    {
        var operations = _context.OperationPatterns.Where(pr=>pr.ProductId==ProductId).AsQueryable();
        return operations;
    }
    
    public async Task<List<Operation>> GetAllOperationsByJobIdFromDB(int JobId)
    {
        return await _context.Operations
            .Where(op => op.JobId == JobId)
            .ToListAsync();
    }

    
    public IQueryable<Operation> GetAllOperationsFromDB()
    {
        var jel = _context.JobEmployees.ToList();
        var operations = _context.Operations.ToList();

   
        
        foreach (var operation in operations)
        {
            operation.CompletedQuantity = jel
                .Where(emp => emp.OperationId == operation.Id)
                .Sum(emp => emp.CompletedQuantity);
            operation.MissingQuantity = jel
                .Where(emp => emp.OperationId == operation.Id)
                .Sum(emp => emp.MissingQuantity);
        }
    
        _context.SaveChanges();
        return _context.Operations.AsQueryable();
    }
    
    
    public async Task<Operation> GetOperationFromDB(int id)
    {
        return await _context.Operations.FirstOrDefaultAsync(op => op.Id == id);
    }

    public async Task UpdateOperation(Operation operation)
    {
        _context.Entry(operation).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    
    

}





