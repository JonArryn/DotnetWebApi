using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Contracts.Repositories;
using WebApiProject.Contracts.Services;
using WebApiProject.Database;
using WebApiProject.Entities;
using WebApiProject.Models.Requests;
using WebApiProject.Models.Responses;

namespace WebApiProject.Repositories;

public class AuthRepository(Db dbContext, IJwtService jwtService) : IAuthRepository
{
    public Task<UserEntity> LogInUser(UserRequest user)
    {
        throw new NotImplementedException();
    }

    //TODO: Determine if mapping needs to be done and instead of passing in the request we pass in a mapped model?
    //TODO: Need to sort out scalable mapping between requests, responses, and entities
    public async Task<UserEntity> RegisterUser(UserEntity newUser)
    {
        
        await dbContext.Users.AddAsync(newUser);
        await dbContext.SaveChangesAsync();

        return newUser;
    }
    
    public async Task<UserEntity?> GetExistingUser(string userEmail)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
        return user ?? null;
    }

    public async Task<string> GenerateAndSaveRefreshTokenAsync(UserEntity user)
    {
        var refreshToken = GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await dbContext.SaveChangesAsync();
        return refreshToken;
    }

    public async Task<TokenResponse?> RefreshTokensAsync(RefreshTokenRequest request)
    {
        var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
        if (user is null) {
            return null;
        }
        return new TokenResponse
        {
            AccessToken = jwtService.CreateToken(user),
            RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
        };
    }

    public async Task<UserEntity?> ValidateRefreshTokenAsync(int userId, string refreshToken)
    {
        var user = await dbContext.Users.FindAsync(userId);
        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return null;
        }

        return user;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }


}