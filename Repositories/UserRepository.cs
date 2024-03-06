using authorization_service.Data;
using authorization_service.Models;
using Microsoft.EntityFrameworkCore;

namespace authorization_service.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetByIdAsync(string id)
    {
        return await _context.Users.FindAsync(id);
    }
    
    public async Task<User> GetByEmailAsync(string email)
    {
        // Используем LINQ для поиска пользователя по электронной почте
        // Предположим, что в вашей базе данных есть таблица Users, содержащая пользователей
        // И поле электронной почты пользователя называется Email

        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(string id)
    {
        var userToDelete = await _context.Users.FindAsync(id);
        if (userToDelete != null)
        {
            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }
    }
}