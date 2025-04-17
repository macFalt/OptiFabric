using Microsoft.EntityFrameworkCore;
using OptiFabricMVC.Domain.Interfaces;

namespace OptiFabricMVC.Infrastructure.Repositories;

public class GenericRepository<TEntity,TKey> : IGenericRepository<TEntity,TKey> where TEntity : class, IEntity<TKey>
{
    private readonly Context _context;

    public GenericRepository(Context context)
    {
        _context = context;
    }
    
    public async Task<TKey> AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<TEntity?> GetByIdAsync(TKey id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }
    
    public async Task UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TKey id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity != null)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>()
            .AsNoTracking()
            .ToListAsync();
    }

    public IQueryable<TEntity> GetAll()
    {
        return _context.Set<TEntity>();
    }
}