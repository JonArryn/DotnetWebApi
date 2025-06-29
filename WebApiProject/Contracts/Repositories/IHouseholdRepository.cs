using WebApiProject.Entities;

namespace WebApiProject.Contracts.Repositories;

public interface IHouseholdRepository
{
    public Task<IEnumerable<HouseholdMember>> GetHouseholdMembersAsync();

    public Task<Household> CreateHouseholdAsync(Household household);
}