namespace OptiFabricMVC.Domain.Interfaces;

public interface IGenericRepository<TEntity,TKey> where TEntity : class, IEntity<TKey>
{
    Task<TKey> AddAsync(TEntity entity);
    Task<TEntity?> GetByIdAsync(TKey id);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TKey id);
    Task<List<TEntity>> GetAllAsync();

    IQueryable<TEntity> GetAll();

}