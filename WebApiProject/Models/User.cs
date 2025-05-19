using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Models;

public class User
{
    public int Id { get; set; }

    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;
    
    [Required]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    public string LastName { get; set; } = string.Empty;

}