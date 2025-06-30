using AutoMapper;
using WebApiProject.Contracts.Repositories;
using WebApiProject.Contracts.Services;
using WebApiProject.DataTransfers.Requests;
using WebApiProject.DataTransfers.Responses;
using WebApiProject.Entities;
using WebApiProject.Utilities;

namespace WebApiProject.Services;

public class HouseholdService : AppBaseService<Household, IHouseholdRepository>, IHouseholdService
{
    private readonly IUserRepository _userRepository;

    public HouseholdService(IHouseholdRepository householdRepository, IUserRepository userRepository, IMapper mapper) :
        base(householdRepository, mapper)
    {
        _userRepository = userRepository;
    }

    public async Task<CreateHouseholdResponse?> CreateHouseholdAsync(CreateHouseholdRequest request)
    {
        var owner = await _userRepository.GetUserByIdAsync(request.OwnerId);
        if (owner is null)
            // TODO implement custom exception type and implement exception handling middleware
            throw new EntityNotFoundException($"User not found by the ID {request.OwnerId} when creating household",
                404);

        request.OwnerId = owner.Id;

        var newHousehold = Mapper.Map<Household>(request);
        newHousehold.Owner = owner;

        // Create the owner as a member with the Owner role
        // TODO determine if we should create a mapping for this
        var ownerMembership = new HouseholdMember
        {
            UserId = owner.Id,
            User = owner,
            Tenant = newHousehold,
            Role = HouseholdRole.Owner,
            JoinedAt = DateTime.UtcNow
        };

        newHousehold.Members.Add(ownerMembership);

        var createdHousehold = await Repository.CreateHouseholdAsync(newHousehold);

        return Mapper.Map<CreateHouseholdResponse>(createdHousehold);
    }

    public async Task<IEnumerable<HouseholdMember>> GetAllHouseholdMembersAsync()
    {
        return await Repository.GetHouseholdMembersAsync();
    }
}