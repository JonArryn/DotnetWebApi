using WebApiProject.DataTransfers.Requests;
using WebApiProject.DataTransfers.Responses;
using WebApiProject.Entities;

namespace WebApiProject.Contracts.Repositories;

public interface IAuthRepository
{

    public Task<User> RegisterUser(User newUser);

    public Task<User?> GetExistingUser(string userEmail);

    public Task<string> GenerateAndSaveRefreshTokenAsync(User user);

    public Task<TokenResponse?> RefreshTokensAsync(RefreshTokenRequest request);

}