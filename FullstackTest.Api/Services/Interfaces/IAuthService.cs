using FullstackTest.Api.DTOs;

namespace FullstackTest.Api.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse?> AuthenticateAsync(LoginRequest request);

    Task<string?> RegisterAsync(RegisterUserRequest request);
}
