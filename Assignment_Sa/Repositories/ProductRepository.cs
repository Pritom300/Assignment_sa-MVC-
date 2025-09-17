using Assignment_Sa.Data;
using Assignment_Sa.Interfaces;
using Assignment_Sa.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment_Sa.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Product>> GetAllAsync() =>
            await _context.Products.ToListAsync();

        public async Task<Product?> GetByIdAsync(int id) =>
            await _context.Products.FindAsync(id);


        public async Task UpdateStockAsync(int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.Stock = product.Stock - quantity;
                await _context.SaveChangesAsync();
            }
        }
    }
}
