using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.Contracts.Repositories;
using WebApiProject.Contracts.Services;
using WebApiProject.DataTransfers.Requests;
using WebApiProject.DataTransfers.Responses;


namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthRepository authRepository,IAuthService authService, IJwtService jwtService) : ControllerBase
    {
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<RegisterResponse>> Register(RegisterRequest request)
        {
            var newUser = await authService.RegisterUser(request);
            if (newUser == null)
            {
                return BadRequest("Could Not Register User");
            }
            
            return Ok(newUser);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenResponse>> Login(LoginRequest request)
        {
            var loginUser = await authService.LogInUser(request);
            
            if (loginUser == null)
            {
                return BadRequest("Could not log in");
            }

            return Ok(loginUser);
        }

        [HttpPost("refreshToken")]
        public async Task<ActionResult<TokenResponse>> RefreshToken(RefreshTokenRequest request)
        {
            var result = await authService.RefreshTokens(request);
            if (result is null)
            {
                return Unauthorized("Invalid token");
            }

            return Ok(result);
        }
        
        [HttpGet("testAuth")]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("You are authenticated");
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet("testAuthZ")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("You are authorized as an admin");
        }
    }
}
