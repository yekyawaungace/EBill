using TravelInsurance.Infrastructure.Entities;

namespace TravelInsurance.Infrastructure.IRepositories
{
    public interface IMerchantsRepository : IRepository<Merchant>
    {
        Task<List<Merchant>> GetAllAsync();

        List<Merchant> GetAll();

        Task<Merchant> GetAsync(Guid id);

        Task<Merchant> AddAsync(Merchant model);
        Task<Merchant> UpdateAsync(Merchant model);

        Task<bool> DeleteAsync(Guid? id);
    }
}
