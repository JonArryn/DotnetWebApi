using System.ComponentModel.DataAnnotations;

namespace WebApiProject.DataTransfers.Responses;

public class GetUserResponse
{
    public Guid Id;
    [EmailAddress] public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}