namespace FullstackTest.Front.DTOs.Response;

public class AuthResponse
{
    public string Token { get; set; }
    public string Username { get; set; }
    public Guid UserId { get; set; }
}
