using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Entities;

public class UserEntity : BaseEntity
{
    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;
    [Required] public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Roles { get; set; } = string.Empty;
    
    public string? RefreshToken { get; set; }
    
    public DateTime? RefreshTokenExpiryTime { get; set; }

}