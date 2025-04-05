using FullstackTest.Api.Data;
using FullstackTest.Api.DTOs;
using FullstackTest.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullstackTest.Api.Controllers;

//[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ApplicationDbContext _context;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] RegisterUserRequest request)
    {
        var token = await _authService.RegisterAsync(request);

        if (token == null)
            return BadRequest(new { message = "E-mail ou usuário já existe." });

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email || u.Username == request.Username);

        var response = new AuthResponse
        {
            Token = token,
            Username = user.Username,
            UserId = user.Id
        };

        return Ok(response);
    }


    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] LoginRequest request)
    {
        var token = await _authService.AuthenticateAsync(request);

        if (token == null)
            return Unauthorized(new { message = "Usuário ou senha inválidos." });

        return Ok(new { token });
    }

}
