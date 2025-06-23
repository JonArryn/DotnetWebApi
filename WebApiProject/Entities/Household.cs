using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Entities;

public class Household : BaseEntity
{
     [Required] [MaxLength(25)] public string Name { get; set; }
     
     [Required] public Guid OwnerId { get; set; }
     [Required] public User Owner { get; set; }

     public ICollection<HouseholdMember> Members { get; } = new List<HouseholdMember>();

}