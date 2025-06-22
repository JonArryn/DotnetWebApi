using Microsoft.EntityFrameworkCore;
using WebApiProject.Entities;

namespace WebApiProject.Database;

public class Db : DbContext
{
    // public DbSet<Card> Cards { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Household> Households { get; set; }
    public DbSet<HouseholdMember> HouseholdMembers { get; set; }
    
    private readonly ILogger<Db> _logger;
    
    public Db(DbContextOptions<Db> options, ILogger<Db> logger)
        : base(options)
    {
        _logger = logger;
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