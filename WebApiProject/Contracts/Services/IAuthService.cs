using WebApiProject.DataTransfers.Requests;
using WebApiProject.DataTransfers.Responses;

namespace WebApiProject.Contracts.Services;

public interface IAuthService
{
    public Task<RegisterResponse?> RegisterUser(RegisterRequest request);

    public Task<LoginResponse?> LogInUser(LoginRequest request);

    public Task<TokenResponse?> RefreshTokens(RefreshTokenRequest request);
}