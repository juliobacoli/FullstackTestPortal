namespace FullstackTest.Api.DTOs.Request;

public class LoginRequest
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
}
