using WebApiProject.Entities;
using WebApiProject.Models.Requests;
using WebApiProject.Models.Responses;

namespace WebApiProject.Contracts.Repositories;

public interface IAuthRepository
{

    public Task<User> RegisterUser(User newUser);

    public Task<User?> GetExistingUser(string userEmail);

    public Task<string> GenerateAndSaveRefreshTokenAsync(User user);

    public Task<TokenResponse?> RefreshTokensAsync(RefreshTokenRequest request);

}