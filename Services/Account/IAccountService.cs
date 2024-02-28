using authorization_service.Models;
using authorization_service.Models.User_DTOs;

namespace authorization_service.Services;

public interface IAccountService
{
    Task<User> RegisterAsync(UserRegistrationDto model);
    Task<User> AuthenticateAsync(UserLoginDto model);
    Task DeleteAsync(string userId);
}