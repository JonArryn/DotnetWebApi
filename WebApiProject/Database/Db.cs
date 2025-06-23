using Microsoft.EntityFrameworkCore;
using WebApiProject.Contracts.Services;
using WebApiProject.Entities;
using WebApiProject.Extensions;

namespace WebApiProject.Database;

public class Db : DbContext
{
    // public DbSet<Card> Cards { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Household> Households { get; set; }
    public DbSet<HouseholdMember> HouseholdMembers { get; set; }
    
    private readonly ILogger<Db> _logger;
    private readonly ITenantProviderService _tenantProviderService;
    
    public Db(DbContextOptions<Db> options, ILogger<Db> logger, ITenantProviderService tenantProviderService)
        : base(options)
    {
        _logger = logger;
        _tenantProviderService = tenantProviderService;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        // Apply tenant configuration to all tenant entities
        modelBuilder.ApplyTenantConfiguration(_tenantProviderService, _logger);
    
        // Other configurations...
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _logger.LogInformation("Configuring database connection");
        base.OnConfiguring(optionsBuilder);
    }
    
    public override int SaveChanges()
    {
        _logger.LogInformation("Saving changes to database");
        return base.SaveChanges();
    }
}