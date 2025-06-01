using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleProject.Data;
using SampleProject.DTO;
using SampleProject.Models;

namespace SampleProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly EduSyncContext _context;

    public AuthController(EduSyncContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null || !VerifyPasswordHash(request.Password, user.PasswordHash))
        {
            return Unauthorized("Invalid email or password");
        }

        // Return user information
        return new LoginResponse
        {
            Email = user.Email!,
            Role = user.Role!
        };
    }

    private static bool VerifyPasswordHash(string password, string? storedHash)
    {
        if (string.IsNullOrEmpty(storedHash))
            return false;

        // In a real application, you should use a proper password hashing library
        // like BCrypt or ASP.NET Core Identity's PasswordHasher
        return password == storedHash; // Simple comparison for demo purposes
    }
}
