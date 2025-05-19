using WebApiProject.Models;
using WebApiProject.Repositories.User;

namespace WebApiProject.Services.UserService;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository userRepository, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        _logger.LogInformation("Service: Getting all users");
        return await _userRepository.GetAllUsersAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        _logger.LogInformation("Service: Getting user by ID: {Id}", id);
        return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        _logger.LogInformation("Service: Getting user by email: {Email}", email);
        return await _userRepository.GetUserByEmailAsync(email);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        _logger.LogInformation("Service: Creating new user");
        // Check if email already exists
        var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
        if (existingUser != null)
        {
            _logger.LogWarning("Attempted to create user with existing email: {Email}", user.Email);
            throw new InvalidOperationException($"User with email {user.Email} already exists");
        }
        
        return await _userRepository.CreateUserAsync(user);
    }

    public async Task<User?> UpdateUserAsync(User user)
    {
        _logger.LogInformation("Service: Updating user with ID: {Id}", user.Id);
        return await _userRepository.UpdateUserAsync(user);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        _logger.LogInformation("Service: Deleting user with ID: {Id}", id);
        return await _userRepository.DeleteUserAsync(id);
    }
}