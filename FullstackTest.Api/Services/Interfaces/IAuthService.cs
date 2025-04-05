using FullstackTest.Api.DTOs.Request;
using FullstackTest.Api.DTOs.Response;

namespace FullstackTest.Api.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse?> AuthenticateAsync(LoginRequest request);

    Task<string?> RegisterAsync(RegisterUserRequest request);
}
