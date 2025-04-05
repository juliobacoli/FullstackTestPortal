using FullstackTest.Api.DTOs;

namespace FullstackTest.Api.Services;

public interface IAuthService
{
    Task<AuthResponse?> AuthenticateAsync(LoginRequest request);

    Task<string?> RegisterAsync(RegisterUserRequest request);
}
