using System.ComponentModel.DataAnnotations;
using WebApiProject.Entities;

namespace WebApiProject.DataTransfers.Requests;

public class CreateHouseholdRequest
{
    [Required] [MaxLength(25)] public string Name { get; set; }
     
    [Required] public Guid OwnerId { get; set; }
    
    // public User Owner { get; set; }
}