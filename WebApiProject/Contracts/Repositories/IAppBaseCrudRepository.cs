namespace WebApiProject.Contracts.Repositories;

public interface IAppBaseCrudRepository<TEntity, TId> : IAppBaseRepository<TEntity, TId> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(TId id);
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity?> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(TId id);
}