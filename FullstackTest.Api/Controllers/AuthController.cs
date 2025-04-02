using FullstackTest.Api.DTOs;
using FullstackTest.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FullstackTest.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] RegisterUserRequest request)
    {
        var success = await _authService.RegisterAsync(request);

        if (!success)
            return BadRequest(new { message = "E-mail ou usuário já existe." });

        return Ok(new { message = "Usuário registrado com sucesso!" });
    }
}
