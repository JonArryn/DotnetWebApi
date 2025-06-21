using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using WebApiProject.Entities;
using WebApiProject.Contracts.Services;

namespace WebApiProject.Services;

public  class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public  string HashPassword(UserEntity userEntity, string password)
    {
        return new PasswordHasher<UserEntity>()
            .HashPassword(userEntity, password);
    }

    public bool VerifyPassword(UserEntity userEntity, string password)
    {
        return new PasswordHasher<UserEntity>().VerifyHashedPassword(userEntity, userEntity.PasswordHash, password) ==
               PasswordVerificationResult.Success;
    }
    
    public string CreateToken(UserEntity userEntity)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userEntity.Email),
            new Claim(ClaimTypes.NameIdentifier, userEntity.Id.ToString()),
            new Claim(ClaimTypes.Role, userEntity.Roles)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
            audience: _configuration.GetValue<string>("AppSettings:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}