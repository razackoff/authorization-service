using authorization_service.Models.User_DTOs;
using authorization_service.Services;
using authorization_service.Services.JWT;
using Microsoft.AspNetCore.Mvc;

namespace authorization_service.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IJwtService _jwtService;

    public AccountController(IAccountService accountService, IJwtService jwtService)
    {
        _accountService = accountService;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegistrationDto model)
    {
        var user = await _accountService.RegisterAsync(model);
        // Генерируем JWT токен для нового пользователя
        var token = _jwtService.GenerateJwtToken(user);
        return Ok(new { user, token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto model)
    {
        var user = await _accountService.AuthenticateAsync(model);
        if (user == null)
        {
            return Unauthorized();
        }

        // Генерируем JWT токен для аутентифицированного пользователя
        var token = _jwtService.GenerateJwtToken(user);
        return Ok(new { user, token });
    }

    // Другие методы контроллера
}