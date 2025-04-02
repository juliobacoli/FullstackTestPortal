using FullstackTest.Api.DTOs;

namespace FullstackTest.Api.Services;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterUserRequest request);
}
