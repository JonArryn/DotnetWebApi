using AutoMapper;

namespace WebApiProject.Services;

public class AppBaseService<TEntity, TRepository>
{
    protected readonly TRepository Repository;
    protected readonly IMapper Mapper;

    public AppBaseService(TRepository repository, IMapper mapper)
    {
        Repository = repository;
        Mapper = mapper;
    }
    
}