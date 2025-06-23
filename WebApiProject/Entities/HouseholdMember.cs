using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Entities;

public class HouseholdMember : TenantBaseEntity
{
    [Required] public Guid UserId { get; set; }
    [Required] public User User { get; set; }
    
    [Required] public Household Tenant { get; set; }
    
    [Required] public HouseholdRole Role { get; set; }
    [Required] public DateTime JoinedAt { get; set; }
}

public enum HouseholdRole
{
    Owner,
    Member
}