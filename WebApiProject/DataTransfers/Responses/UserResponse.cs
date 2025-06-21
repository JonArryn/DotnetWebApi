using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Models.Responses;

public class UserResponse
{
    public Guid Id;
    [EmailAddress] public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}