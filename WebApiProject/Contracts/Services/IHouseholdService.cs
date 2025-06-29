using WebApiProject.DataTransfers.Requests;
using WebApiProject.DataTransfers.Responses;
using WebApiProject.Entities;

namespace WebApiProject.Contracts.Services;

public interface IHouseholdService
{
    public Task<CreateHouseholdResponse?> CreateHouseholdAsync(CreateHouseholdRequest request);
    public Task<IEnumerable<HouseholdMember>> GetAllHouseholdMembersAsync();
}