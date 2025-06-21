namespace WebApiProject.Contracts.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<Entities.UserEntity>> GetAllUsersAsync();
    Task<Entities.UserEntity?> GetUserByIdAsync(int id);
    Task<Entities.UserEntity?> GetUserByEmailAsync(string email);
    Task<Entities.UserEntity> CreateUserAsync(Entities.UserEntity userEntity);
    Task<Entities.UserEntity?> UpdateUserAsync(Entities.UserEntity userEntity);
    Task<bool> DeleteUserAsync(int id);
}