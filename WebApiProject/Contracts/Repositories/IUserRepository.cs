using WebApiProject.Entities;

namespace WebApiProject.Contracts.Repositories;

public interface IUserRepository : IAppBaseRepository<User, Guid>
{
    Task<User?> GetUserByEmailAsync(string email);

    public Task<User?> GetUserByIdAsync(Guid userId);

}