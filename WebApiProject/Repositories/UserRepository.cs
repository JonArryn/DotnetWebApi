using Microsoft.EntityFrameworkCore;
using WebApiProject.Contracts.Repositories;
using WebApiProject.Database;
using WebApiProject.Contracts.Services;
using WebApiProject.Entities;

namespace WebApiProject.Repositories;

public class UserRepository : AppBaseRepository<User, Guid>, IUserRepository
{
    public UserRepository(Db context, ILogger<User> logger)
        : base(context, logger)
    {
    }


    public async Task<User?> GetUserByEmailAsync(string email)
    {
        Logger.LogInformation("Getting user with email: {Email}", email);
        return await DbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        Logger.LogInformation("Getting user with id: {Id}", userId);
        return await DbSet.FirstOrDefaultAsync(u => u.Id == userId);
    }

   
}