using Assignment_Sa.Models;

namespace Assignment_Sa.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task UpdateStockAsync(int productId, int quantity);
    }
}
