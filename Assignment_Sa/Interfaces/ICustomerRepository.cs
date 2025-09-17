using Assignment_Sa.Models;

namespace Assignment_Sa.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
    }
}
