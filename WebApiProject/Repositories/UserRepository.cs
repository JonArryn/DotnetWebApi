using Microsoft.EntityFrameworkCore;
using WebApiProject.Contracts.Repositories;
using WebApiProject.Database;
using WebApiProject.Contracts.Services;

namespace WebApiProject.Repositories;

public class UserRepository : IUserRepository
{
    private readonly Db _context;
    private readonly ILogger<UserRepository> _logger;
    
    public UserRepository(Db context, ILogger<UserRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<IEnumerable<Entities.UserEntity>> GetAllUsersAsync()
    {
        _logger.LogInformation("Fetching all users from the database");
        return await _context.Users.ToListAsync();
    }
    
    public async Task<Entities.UserEntity?> GetUserByIdAsync(int id)
    {
        _logger.LogInformation("Getting user with ID: {Id}", id);
        return await _context.Users.FindAsync(id);
    }
    
    public async Task<Entities.UserEntity?> GetUserByEmailAsync(string email)
    {
        _logger.LogInformation("Getting user with email: {Email}", email);
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Entities.UserEntity> CreateUserAsync(Entities.UserEntity userEntity)
    {
        _logger.LogInformation("Creating new user with email: {Email}", userEntity.Email);
        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
        return userEntity;
    }
    
    public async Task<Entities.UserEntity?> UpdateUserAsync(Entities.UserEntity userEntity)
    {
        _logger.LogInformation("Updating user with ID: {Id}", userEntity.Id);
        var existingUser = await _context.Users.FindAsync(userEntity.Id);
        if (existingUser == null) return null;

        _context.Entry(existingUser).CurrentValues.SetValues(userEntity);
        await _context.SaveChangesAsync();
        return existingUser;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        _logger.LogInformation("Deleting user with ID: {Id}", id);
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}