using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Contracts.Services;

namespace WebApiProject.Entities;

public class TenantBaseEntity : BaseEntity
{
   
   [Required] public Guid TenantId { get; set; }

  
}