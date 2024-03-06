using authorization_service.DTOs;
using authorization_service.Services;
using authorization_service.Services.JWT;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace authorization_service.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IJwtService _jwtService;
    private readonly IValidator<UserRegistrationDto> _registrationValidator;
    private readonly IValidator<UserLoginDto> _loginValidator;
    private readonly IValidator<UserPasswordChangeDto> _passwordChangeValidator;

    public AccountController(IAccountService accountService, IJwtService jwtService,
        IValidator<UserRegistrationDto> registrationValidator,
        IValidator<UserLoginDto> loginValidator,
        IValidator<UserPasswordChangeDto> passwordChangeValidator) // Добавляем валидатор смены пароля
    {
        _accountService = accountService;
        _jwtService = jwtService;
        _registrationValidator = registrationValidator;
        _loginValidator = loginValidator;
        _passwordChangeValidator = passwordChangeValidator; // Инициализируем валидатор смены пароля
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegistrationDto model)
    {
        var validationResult = _registrationValidator.Validate(model);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        
        var user = await _accountService.RegisterAsync(model);
        // Генерируем JWT токен для нового пользователя
        var token = _jwtService.GenerateJwtToken(user);
        return Ok(new { user, token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto model)
    {
        var validationResult = _loginValidator.Validate(model);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        
        var user = await _accountService.AuthenticateAsync(model);
        
        if (user == null)
        {
            // Возвращаем сообщение об ошибке
            return BadRequest("Incorrect email or password.");
        }

        // Генерируем JWT токен для аутентифицированного пользователя
        var token = _jwtService.GenerateJwtToken(user);
        return Ok(new { user, token });
    }
    
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(UserPasswordChangeDto model)
    {
        var validationResult = _passwordChangeValidator.Validate(model);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
    
        try
        {
            await _accountService.ChangePasswordAsync(model);
            return Ok("Password changed successfully.");
        }
        catch (ApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}