using TravelInsurance.Infrastructure.Entities;

namespace TravelInsurance.Infrastructure.IRepositories
{
    public interface IPremiumFeesRepository : IRepository<PremiumFee>
    {
        Task<List<PremiumFee>> GetAllAsync();

        List<PremiumFee> GetAll();

        Task<PremiumFee> GetAsync(Guid id);

        Task<PremiumFee> AddAsync(PremiumFee model);
        Task<PremiumFee> UpdateAsync(PremiumFee model);

        Task<bool> DeleteAsync(Guid id);
    }
}
