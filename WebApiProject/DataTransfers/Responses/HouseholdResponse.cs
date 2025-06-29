using WebApiProject.Entities;

namespace WebApiProject.DataTransfers.Responses;

public class CreateHouseholdResponse : BaseResponse
{
    
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
    
}

public class GetHouseholdMemberResponse : BaseResponse
{
    public Guid UserId { get; set; }
    public HouseholdRole Role { get; set; }
    public DateTime JoinedAt { get; set; }
    public GetUserResponse User { get; set; }
}