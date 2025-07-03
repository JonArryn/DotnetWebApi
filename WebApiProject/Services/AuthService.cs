using AutoMapper;
using WebApiProject.Contracts.Repositories;
using WebApiProject.Contracts.Services;
using WebApiProject.DataTransfers.Requests;
using WebApiProject.DataTransfers.Responses;
using WebApiProject.Entities;
using WebApiProject.Utilities;
using static WebApiProject.Utilities.ErrorCatalog;

namespace WebApiProject.Services;

public class AuthService(IAuthRepository authRepository, IJwtService jwtService, IMapper mapper) : IAuthService
{
    public async Task<RegisterResponse> RegisterUser(RegisterRequest request)
    {
        var userExists = await authRepository.GetExistingUser(request.Email) != null;
        if (userExists)
        {
            throw new CouldNotAuthenticateException("Could not register user at this time.", AuthErrorCodes.USER_ALREADY_EXISTS);
        }

        var newUser = mapper.Map<User>(request);
        newUser.PasswordHash = jwtService.HashPassword(newUser, request.Password);

        var registeredUser = await authRepository.RegisterUser(newUser);

        return mapper.Map<RegisterResponse>(registeredUser);
    }

    public async Task<LoginResponse> LogInUser(LoginRequest request)
    {
        var loginUser = await authRepository.GetExistingUser(request.Email);
        if (loginUser is null)
        {
            throw new CouldNotAuthenticateException(errorCode: AuthErrorCodes.INVALID_CREDENTIALS);
        }
        var passwordVerified = jwtService.VerifyPassword(loginUser, request.Password);
        if (!passwordVerified)
        {
            throw new CouldNotAuthenticateException("Could not log in.");
        }
        var response = mapper.Map<LoginResponse>(loginUser);

        response.AccessToken = jwtService.CreateToken(loginUser);
        response.RefreshToken = await authRepository.GenerateAndSaveRefreshTokenAsync(loginUser);

        return response;
    }

    public async Task<TokenResponse?> RefreshTokens(RefreshTokenRequest request)
    {
        // TODO: figure out how to properly use these refresh tokens
        // TODO: convert null return to http exception
        var result = await authRepository.RefreshTokensAsync(request);
        if (result?.AccessToken is null || result?.RefreshToken is null) return null;

        return result;
    }
}