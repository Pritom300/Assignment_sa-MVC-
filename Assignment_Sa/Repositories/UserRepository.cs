using Assignment_Sa.Data;
using Assignment_Sa.Interfaces;
using Assignment_Sa.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Assignment_Sa.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AnyAsync()
        {
            return await _context.Users.AnyAsync();
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

     

        public async Task<User?> GetByUsernameAndPasswordAsync(string username, string passwordHash)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == passwordHash);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
