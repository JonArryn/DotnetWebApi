using WebApiProject.Entities;

namespace WebApiProject.Contracts.Services;

public interface IJwtService
{
    public string HashPassword(UserEntity userEntity, string password);

    public bool VerifyPassword(UserEntity userEntity, string password);

    public string CreateToken(UserEntity userEntity);
}