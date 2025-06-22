using WebApiProject.Entities;

namespace WebApiProject.Contracts.Repositories;

public interface IUserRepository : IBaseRepository<User, Guid>
{
    Task<User?> GetUserByEmailAsync(string email);

}