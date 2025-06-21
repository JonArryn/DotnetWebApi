using AutoMapper;
using WebApiProject.Contracts.Repositories;
using WebApiProject.Contracts.Services;
using WebApiProject.Entities;
using WebApiProject.Models.Requests;
using WebApiProject.Models.Responses;

namespace WebApiProject.Services;

public class AuthService(IAuthRepository authRepository, IJwtService jwtService, IMapper mapper): IAuthService
{
    
    public async Task<RegisterResponse?> RegisterUser(RegisterRequest request)
    {
        var userExists = await authRepository.GetExistingUser(request.Email) != null;
        if (userExists)
        {
            return null;
        }

        var newUser = mapper.Map<UserEntity>(request);
        newUser.PasswordHash = jwtService.HashPassword(newUser, request.Password); 
        
        var registeredUser = await authRepository.RegisterUser(newUser);

        return mapper.Map<RegisterResponse>(registeredUser);
    }

    public async Task<LoginResponse?> LogInUser(LoginRequest request)
    {
        var loginUser = await authRepository.GetExistingUser(request.Email);
        if (loginUser is null)
        {
            return null;
        }

        if (jwtService.VerifyPassword(loginUser, request.Password) == false)
        {
            return null;
        }

        var response = mapper.Map<LoginResponse>(loginUser);

        response.AccessToken = jwtService.CreateToken(loginUser);
        response.RefreshToken = await authRepository.GenerateAndSaveRefreshTokenAsync(loginUser);

        return response;
    }

    public async Task<TokenResponse?> RefreshTokens(RefreshTokenRequest request)
    {
        var result = await authRepository.RefreshTokensAsync(request);
        if (result?.AccessToken is null || result?.RefreshToken is null)
        {
            return null;
        }

        return result;
    }

}