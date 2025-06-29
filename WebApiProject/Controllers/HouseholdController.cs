using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.Contracts.Services;
using WebApiProject.DataTransfers.Requests;
using WebApiProject.Entities;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseholdController : AppBaseController<IHouseholdService>
    {
        public HouseholdController(IHouseholdService service) : base(service)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateHouseholdRequest request)
        {
            request.OwnerId = this.GetUserId();
            
            var household = await Service.CreateHouseholdAsync(request);
            return Ok(household);
        }

        [HttpGet("members")]
        public async Task<ActionResult<IEnumerable<HouseholdMember>>> GetHouseholdMembers()
        {
            var members = await Service.GetAllHouseholdMembersAsync();
            return Ok(members);
        }
    }
}
