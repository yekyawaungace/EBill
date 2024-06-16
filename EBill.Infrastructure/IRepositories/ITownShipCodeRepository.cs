using TravelInsurance.Infrastructure.Entities;

namespace TravelInsurance.Infrastructure.IRepositories
{
    public interface ITownShipCodeRepository : IRepository<TownShipCode>
    {
        Task<List<TownShipCode>> GetAllAsync();

        List<TownShipCode> GetAll();

        Task<TownShipCode> GetAsync(Guid id);

        Task<TownShipCode> AddAsync(TownShipCode model);
        Task<TownShipCode> UpdateAsync(TownShipCode model);

        Task<bool> DeleteAsync(Guid id);
    }
}
