namespace FullstackTest.Api.DTOs;

public class AuthResponse
{
    public string Token { get; set; }
    public string Username { get; set; }
    public Guid UserId { get; set; }
}
