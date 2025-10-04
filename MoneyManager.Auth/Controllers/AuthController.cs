using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.Auth.DTO;
using MoneyManager.Auth.Entities;
using MoneyManager.Auth.Services;

namespace MoneyManager.Auth.Controllers;

[Controller]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _users;
    private readonly TokenService _tokens;

    public AuthController(UserManager<AppUser> users, TokenService tokens)
    {
        _users = users;
        _tokens = tokens;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto, CancellationToken ct)
    {
        var user = new AppUser { Id = Guid.NewGuid(), Email = dto.Email, UserName = dto.Email };
        var res = await _users.CreateAsync(user, dto.Password);
        if (!res.Succeeded) return BadRequest(res.Errors);
        return NoContent();
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResultDto>> Login([FromBody] LoginDto dto, CancellationToken ct)
    {
        var user = await _users.FindByEmailAsync(dto.Email);
        if (user is null) return Unauthorized();

        var ok = await _users.CheckPasswordAsync(user, dto.Password);
        if (!ok) return Unauthorized();

        var access = _tokens.Issue(user);
        return new LoginResultDto(access);
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        var sub = User.FindFirst("sub")?.Value;
        var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
        return Ok(new { sub, email });
    }
}