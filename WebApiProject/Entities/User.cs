using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Entities;

public class User : BaseEntity
{
    [Required] [EmailAddress] [MaxLength(99)] public string Email { get; set; } = string.Empty;
    [Required] [MaxLength(99)] public string PasswordHash { get; set; } = string.Empty;
    [MaxLength(50)] public string FirstName { get; set; } = string.Empty;
    [MaxLength(50)] public string LastName { get; set; } = string.Empty;
    public string Roles { get; set; } = "User";
    public string? RefreshToken { get; set; } = null;
    public DateTime? RefreshTokenExpiryTime { get; set; } = null;

    public ICollection<HouseholdMember> HouseholdMemberships { get; set; } = new List<HouseholdMember>();
    public ICollection<Household> OwnedHouseholds { get; set; } = new List<Household>();

}
