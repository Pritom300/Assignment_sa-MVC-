using Assignment_Sa.Models;

namespace Assignment_Sa.Interfaces
{
    public interface ISalesMasterRepository
    {
        Task<IEnumerable<SaleMaster>> GetAllAsync();
        Task<SaleMaster?> GetByIdAsync(int id);
        Task AddAsync(SaleMaster master);

    }
}
