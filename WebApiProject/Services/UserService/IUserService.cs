namespace WebApiProject.Services.UserService;

public interface IUserService
{
    Task<IEnumerable<Models.User>> GetAllUsersAsync();
    Task<Models.User?> GetUserByIdAsync(int id);
    Task<Models.User?> GetUserByEmailAsync(string email);
    Task<Models.User> CreateUserAsync(Models.User user);
    Task<Models.User?> UpdateUserAsync(Models.User user);
    Task<bool> DeleteUserAsync(int id);
}