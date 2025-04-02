using FullstackTest.Api.Data;
using FullstackTest.Api.DTOs;
using FullstackTest.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FullstackTest.Api.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;

    public AuthService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> RegisterAsync(RegisterUserRequest request)
    {
        var exists = await _context.Users.AnyAsync(u =>u.Email == request.Email || u.Username == request.Username);

        if (exists)
            return false;

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new User
        {
            FullName = request.FullName,
            Username = request.Username,
            Email = request.Email,
            PasswordHash = passwordHash
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return true;
    }
}
