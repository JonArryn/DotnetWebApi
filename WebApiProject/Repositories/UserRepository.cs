using Microsoft.EntityFrameworkCore;
using WebApiProject.Contracts.Repositories;
using WebApiProject.Database;
using WebApiProject.Contracts.Services;
using WebApiProject.Entities;

namespace WebApiProject.Repositories;

public class UserRepository : BaseRepository<User, Guid>, IUserRepository
{
    public UserRepository(Db context, ILogger<BaseRepository<User, Guid>> logger)
        : base(context, logger)
    {
    }


    public async Task<User?> GetUserByEmailAsync(string email)
    {
        Logger.LogInformation("Getting user with email: {Email}", email);
        return await DbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

   
}