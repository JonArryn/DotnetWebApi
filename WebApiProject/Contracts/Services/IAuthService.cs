using WebApiProject.Models.Requests;
using WebApiProject.Models.Responses;

namespace WebApiProject.Contracts.Services;

public interface IAuthService
{
    public Task<RegisterResponse?> RegisterUser(RegisterRequest request);

    public Task<LoginResponse?> LogInUser(LoginRequest request);

    public Task<TokenResponse?> RefreshTokens(RefreshTokenRequest request);
}