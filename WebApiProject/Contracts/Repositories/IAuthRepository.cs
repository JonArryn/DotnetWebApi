using WebApiProject.Entities;
using WebApiProject.Models.Requests;
using WebApiProject.Models.Responses;

namespace WebApiProject.Contracts.Repositories;

public interface IAuthRepository
{

    public Task<UserEntity> RegisterUser(UserEntity newUser);

    public Task<UserEntity?> GetExistingUser(string userEmail);

    public Task<string> GenerateAndSaveRefreshTokenAsync(UserEntity user);

    public Task<TokenResponse?> RefreshTokensAsync(RefreshTokenRequest request);

}