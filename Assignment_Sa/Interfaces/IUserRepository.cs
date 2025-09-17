using Assignment_Sa.Models;

namespace Assignment_Sa.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByUsernameAndPasswordAsync(string username, string passwordHash);
        Task AddAsync(User user);
        Task<bool> AnyAsync();
    }
}
