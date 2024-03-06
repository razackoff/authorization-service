using authorization_service.Models;

namespace authorization_service.Repositories;

public interface IUserRepository {
    Task<User> GetByIdAsync(string id);
    Task<User> GetByEmailAsync(string email);
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(string id);
}