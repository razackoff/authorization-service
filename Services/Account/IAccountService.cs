using authorization_service.DTOs;
using authorization_service.Models;

namespace authorization_service.Services;

public interface IAccountService
{
    Task<User> RegisterAsync(UserRegistrationDto model);
    Task<User> AuthenticateAsync(UserLoginDto model);
    Task DeleteAsync(string userId);
}