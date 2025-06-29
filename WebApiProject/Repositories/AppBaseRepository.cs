using Microsoft.EntityFrameworkCore;
using WebApiProject.Contracts.Repositories;
using WebApiProject.Database;

namespace WebApiProject.Repositories;

public class AppBaseRepository<TEntity, TId> : IAppBaseRepository<TEntity, TId>
    where TEntity : class
{
    
    protected readonly Db Context;
    protected readonly ILogger<TEntity> Logger;
    protected readonly DbSet<TEntity> DbSet;

    protected AppBaseRepository(Db context, ILogger<TEntity>
        logger)
    {
        Context = context;
        Logger = logger;
        DbSet = context.Set<TEntity>();
    }
    
    

}