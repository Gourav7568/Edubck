namespace SampleProject.DTO;

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginResponse
{
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
