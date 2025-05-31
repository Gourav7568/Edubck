using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleProject.Data;
using SampleProject.DTO;
using SampleProject.Models;
using SampleProject.Services;

namespace SampleProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly EduSyncContext _context;
    private readonly IJwtService _jwtService;

    public AuthController(EduSyncContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
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

        var token = _jwtService.GenerateToken(user);

        return new LoginResponse
        {
            Token = token,
            Email = user.Email!,
            Role = user.Role!,
            Expiration = DateTime.UtcNow.AddDays(7) // Matches the JWT expiration
        };
    }

    private static bool VerifyPasswordHash(string password, string? storedHash)
    {
        if (string.IsNullOrEmpty(storedHash))
            return false;

        // In a real application, you should use a proper password hashing library
        // like BCrypt or ASP.NET Core Identity's PasswordHasher
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        
        return hash == storedHash.ToLower();
    }
}
