using Microsoft.AspNetCore.Mvc;
using WebApiProject.Contracts.Services;
using WebApiProject.DataTransfers.Requests;
using WebApiProject.DataTransfers.Responses;
using WebApiProject.Entities;

namespace WebApiProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HouseholdController : AppBaseController<IHouseholdService>
{
    public HouseholdController(IHouseholdService service) : base(service)
    {
    }

    [HttpPost]
    public async Task<ActionResult<CreateHouseholdResponse>> Create([FromBody] CreateHouseholdRequest request)
    {
        request.OwnerId = GetUserId();

        var newHousehold = await Service.CreateHouseholdAsync(request);
        return Ok(newHousehold);
    }

    [HttpGet("members")]
    public async Task<ActionResult<IEnumerable<HouseholdMember>>> GetHouseholdMembers()
    {
        var members = await Service.GetAllHouseholdMembersAsync();
        return Ok(members);
    }
}