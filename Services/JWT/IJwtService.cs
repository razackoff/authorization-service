using authorization_service.Models;

namespace authorization_service.Services.JWT;

public interface IJwtService
{
    string GenerateJwtToken(User user);
    Task<User> ValidateJwtTokenAsync(string token);
}