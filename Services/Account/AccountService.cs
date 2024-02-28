using authorization_service.Models;
using authorization_service.Models.User_DTOs;
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

        // Проверить, найден ли пользователь и соответствует ли пароль
        if (user != null)
        {
            // Вернуть пользователя в качестве результата аутентификации
            return user;
        }
        else
        {
            // Вернуть null, если аутентификация не удалась
            return null;
        }
    }
    
    
    public async Task DeleteAsync(string userId)
    {
        // Удаление пользователя по его идентификатору
        await _userRepository.DeleteAsync(userId);
    }
}
