using Microsoft.EntityFrameworkCore;
using WebApiProject.Contracts.Repositories;
using WebApiProject.Database;

namespace WebApiProject.Repositories;

public abstract class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId>
    where TEntity : class
{
    
    protected readonly Db Context;
    protected readonly ILogger<BaseRepository<TEntity, TId>> Logger;
    protected readonly DbSet<TEntity> DbSet;

    protected BaseRepository(Db context, ILogger<BaseRepository<TEntity, TId>> logger)
    {
        Context = context;
        Logger = logger;
        DbSet = context.Set<TEntity>();
    }
    
    
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        Logger.LogInformation("Fetching all {EntityName} from the database", typeof(TEntity).Name);
        return await DbSet.ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(TId id)
    {
        Logger.LogInformation("Getting {EntityName} with ID: {Id}", typeof(TEntity).Name, id);
        return await DbSet.FindAsync(id);
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        Logger.LogInformation("Creating new {EntityName}", typeof(TEntity).Name);
        await DbSet.AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity?> UpdateAsync(TEntity entity)
    {
        Logger.LogInformation("Updating {EntityName}", typeof(TEntity).Name);
        DbSet.Update(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(TId id)
    {
        Logger.LogInformation("Deleting {EntityName} with ID: {Id}", typeof(TEntity).Name, id);
        var entity = await GetByIdAsync(id);
        if (entity == null) return false;

        DbSet.Remove(entity);
        await Context.SaveChangesAsync();
        return true;
    }
}