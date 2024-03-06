using System.Security.Authentication;
using authorization_service.DTOs;
using authorization_service.Models;
using authorization_service.Repositories;

namespace authorization_service.Services;

public class AccountService : IAccountService
{
    private readonly IUserRepository _userRepository;

    public AccountService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> RegisterAsync(UserRegistrationDto model)
    {
        // Создание нового объекта User из DTO
        var user = new User
        {
            Email = model.Email,
            Password = model.Password
            // Другие необходимые поля
        };

        user.Id = Guid.NewGuid().ToString();
        
        // Добавление нового пользователя
        return await _userRepository.AddAsync(user);
    }

    public async Task<User> AuthenticateAsync(UserLoginDto model)
    {
        // Получить пользователя по его логину (или email)
        var user = await _userRepository.GetByEmailAsync(model.Email);

        // Проверить, найден ли пользователь
        if (user == null)
        {
            // Если пользователь не найден, возвращаем null
            return null;
        }

        // Проверить соответствие пароля
        if (user.Password != model.Password)
        {
            // Если пароль не совпадает, возвращаем null
            return null;
        }

        // Если пользователь найден и пароль совпадает, возвращаем пользователя
        return user;
    }
    
    
    public async Task ChangePasswordAsync(UserPasswordChangeDto userPasswordChangeDto)
    {
        var user = await _userRepository.GetByEmailAsync(userPasswordChangeDto.Email);
        if (user == null)
        {
            throw new ApplicationException("User not found.");
        }

        // Проверяем текущий пароль
        if (userPasswordChangeDto.CurrentPassword != user.Password)
        {
            throw new ApplicationException("Current password is incorrect.");
        }

        // Обновляем пароль
        user.Password = userPasswordChangeDto.NewPassword;
        await _userRepository.UpdateAsync(user);
    }
    
    public async Task DeleteAsync(string userId)
    {
        // Удаление пользователя по его идентификатору
        await _userRepository.DeleteAsync(userId);
    }
}
