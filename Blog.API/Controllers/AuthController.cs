using Blog.Core.Models.Users;
using Blog.Core.Services;
using Blog.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
    {
        try
        {
            var authResponse = await _authService.Login(loginDto);
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(1) // Set the cookie to expire in 1 day
            };

            // Assuming authResponse contains a property called 'Token' with the JWT
            Response.Cookies.Append("AuthToken", authResponse.Token, cookieOptions);
            return Ok(authResponse);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetUser()
    {
        var user = await _authService.GetUser();
        if (user == null)
        {
            return NotFound("User not found.");
        }
        return Ok(user);
    }
}
