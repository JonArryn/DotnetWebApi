using Microsoft.EntityFrameworkCore;
using WebApiProject.Contracts.Repositories;
using WebApiProject.Database;
using WebApiProject.DataTransfers.Requests;
using WebApiProject.Entities;

namespace WebApiProject.Repositories;

public class HouseholdRepository : AppBaseRepository<Household, Guid>, IHouseholdRepository
{
    public HouseholdRepository(Db context, ILogger<Household> logger)
        : base(context, logger)
    {
    }
    
    public async Task<IEnumerable<HouseholdMember>> GetHouseholdMembersAsync()
    {
        return await Context.HouseholdMembers.ToListAsync();
    }

    public async Task<Household> CreateHouseholdAsync(Household household)
    {
        
        var newHousehold = await DbSet.AddAsync(household);
        await Context.SaveChangesAsync();
        return newHousehold.Entity;
    }
    
    
}