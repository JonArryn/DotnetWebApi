using System.ComponentModel.DataAnnotations;

namespace WebApiProject.DataTransfers.Responses;

public class RegisterResponse
{
    [Required] public Guid Id { get; set; }
    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

public class LoginResponse
{
    [Required] public Guid Id { get; set; }
    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    [Required] public string AccessToken { get; set; }
    [Required] public string RefreshToken { get; set; }
}

public class TokenResponse
{
    [Required] public string AccessToken { get; set; }
    [Required] public string RefreshToken { get; set; }
}
