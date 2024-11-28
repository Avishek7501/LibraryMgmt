using System.ComponentModel.DataAnnotations;

public class LoginRequest
{
    [Required]
    public string Username { get; set; } = string.Empty; // Default to avoid null warnings

    [Required]
    public string Password { get; set; } = string.Empty; // Default to avoid null warnings
}
