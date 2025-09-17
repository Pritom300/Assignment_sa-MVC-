using Assignment_Sa.Data;
using Assignment_Sa.Interfaces;
using Assignment_Sa.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment_Sa.Repositories
{
    public class SalesMasterRepository : ISalesMasterRepository
    {
        private readonly ApplicationDbContext _context;
        public SalesMasterRepository(ApplicationDbContext context) => _context = context;


        public async Task AddAsync(SaleMaster master)
        {
            await _context.SaleMasters.AddAsync(master);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SaleMaster>> GetAllAsync() =>
               await _context.SaleMasters.Include(s => s.Customer).Include(c=>c.CreatedByUser).ToListAsync();

        public async Task<SaleMaster?> GetByIdAsync(int id) =>
             await _context.SaleMasters
                 .Include(s => s.SalesDetails)
                 .ThenInclude(d => d.Product)
                 .Include(c=>c.Customer)
                 .FirstOrDefaultAsync(s => s.SaleId == id);

    }
}
