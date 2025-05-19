using Microsoft.AspNetCore.Mvc;
using WebApiProject.Models;
using WebApiProject.Services.UserService;

namespace WebApiProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
   private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        _logger.LogInformation("HTTP GET /api/users");
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        _logger.LogInformation("HTTP GET /api/users/{Id}", id);
        var user = await _userService.GetUserByIdAsync(id);
        
        if (user == null)
            return NotFound();
            
        return Ok(user);
    }
    [HttpGet("email")]
    public async Task<ActionResult<User>> GetUserByEmail([FromQuery] string email)
    {
        _logger.LogInformation("HTTP GET /api/users/email/{Email}", email);
        var user = await _userService.GetUserByEmailAsync(email);
        
        if (user == null)
            return NotFound();
            
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        _logger.LogInformation("HTTP POST /api/users");
        try
        {
            var createdUser = await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<User>> UpdateUser(int id, User user)
    {
        _logger.LogInformation("HTTP PUT /api/users/{Id}", id);
        if (id != user.Id)
            return BadRequest("ID in URL does not match ID in request body");
            
        var updatedUser = await _userService.UpdateUserAsync(user);
        
        if (updatedUser == null)
            return NotFound();
            
        return Ok(updatedUser);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        _logger.LogInformation("HTTP DELETE /api/users/{Id}", id);
        var result = await _userService.DeleteUserAsync(id);
        
        if (!result)
            return NotFound();
            
        return NoContent();
    }
}