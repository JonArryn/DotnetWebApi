using WebApiProject.Entities;

namespace WebApiProject.Contracts.Services;

public interface IJwtService
{
    public string HashPassword(User user, string password);

    public bool VerifyPassword(User user, string password);

    public string CreateToken(User user);
}