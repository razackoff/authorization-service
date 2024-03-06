using System.ComponentModel.DataAnnotations;

namespace authorization_service.DTOs;

public class UserRegistrationDto
{
    public string Email { get; set; }

    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    
}