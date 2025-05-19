using Microsoft.EntityFrameworkCore;
using WebApiProject.Database;
using WebApiProject.Models;

namespace WebApiProject.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly Db _context;
    private readonly ILogger<UserRepository> _logger;
    
    public UserRepository(Db context, ILogger<UserRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<IEnumerable<Models.User>> GetAllUsersAsync()
    {
        _logger.LogInformation("Fetching all users from the database");
        return await _context.Users.ToListAsync();
    }
    
    public async Task<Models.User?> GetUserByIdAsync(int id)
    {
        _logger.LogInformation("Getting user with ID: {Id}", id);
        return await _context.Users.FindAsync(id);
    }
    
    public async Task<Models.User?> GetUserByEmailAsync(string email)
    {
        _logger.LogInformation("Getting user with email: {Email}", email);
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Models.User> CreateUserAsync(Models.User user)
    {
        _logger.LogInformation("Creating new user with email: {Email}", user.Email);
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }
    
    public async Task<Models.User?> UpdateUserAsync(Models.User user)
    {
        _logger.LogInformation("Updating user with ID: {Id}", user.Id);
        var existingUser = await _context.Users.FindAsync(user.Id);
        if (existingUser == null) return null;

        _context.Entry(existingUser).CurrentValues.SetValues(user);
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