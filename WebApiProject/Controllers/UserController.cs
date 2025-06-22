using Microsoft.AspNetCore.Mvc;
using WebApiProject.Contracts.Repositories;
using WebApiProject.Entities;

namespace WebApiProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
   private readonly IUserRepository _userRepository;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserRepository userRepository, ILogger<UserController> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }
    
    [HttpGet("email")]
    public async Task<ActionResult<User>> GetUserByEmail([FromQuery] string email)
    {
        _logger.LogInformation("HTTP GET /api/users/email/{Email}", email);
        var user = await _userRepository.GetUserByEmailAsync(email);
        
        if (user == null)
            return NotFound();
            
        return Ok(user);
    }

   
}