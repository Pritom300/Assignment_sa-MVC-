using Assignment_Sa.Data;
using Assignment_Sa.Interfaces;
using Assignment_Sa.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment_Sa.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Customer>> GetAllAsync() =>
          await _context.Customers.ToListAsync();

        public async Task<Customer?> GetByIdAsync(int id) =>
            await _context.Customers.FindAsync(id);
    }
}
