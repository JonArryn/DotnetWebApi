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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserEntity>>> GetAllUsers()
    {
        _logger.LogInformation("HTTP GET /api/users");
        var users = await _userRepository.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserEntity>> GetUserById(int id)
    {
        _logger.LogInformation("HTTP GET /api/users/{Id}", id);
        var user = await _userRepository.GetUserByIdAsync(id);
        
        if (user == null)
            return NotFound();
            
        return Ok(user);
    }
    [HttpGet("email")]
    public async Task<ActionResult<UserEntity>> GetUserByEmail([FromQuery] string email)
    {
        _logger.LogInformation("HTTP GET /api/users/email/{Email}", email);
        var user = await _userRepository.GetUserByEmailAsync(email);
        
        if (user == null)
            return NotFound();
            
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserEntity>> CreateUser(UserEntity userEntity)
    {
        _logger.LogInformation("HTTP POST /api/users");
        try
        {
            var createdUser = await _userRepository.CreateUserAsync(userEntity);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UserEntity>> UpdateUser(Guid id, UserEntity userEntity)
    {
        _logger.LogInformation("HTTP PUT /api/users/{Id}", id);
        if (id != userEntity.Id)
            return BadRequest("ID in URL does not match ID in request body");
            
        var updatedUser = await _userRepository.UpdateUserAsync(userEntity);
        
        if (updatedUser == null)
            return NotFound();
            
        return Ok(updatedUser);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        _logger.LogInformation("HTTP DELETE /api/users/{Id}", id);
        var result = await _userRepository.DeleteUserAsync(id);
        
        if (!result)
            return NotFound();
            
        return NoContent();
    }
}