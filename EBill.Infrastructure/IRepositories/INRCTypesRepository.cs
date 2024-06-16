using TravelInsurance.Infrastructure.Entities;

namespace TravelInsurance.Infrastructure.IRepositories
{
    public interface INRCTypesRepository : IRepository<NRCType>
    {
        Task<List<NRCType>> GetAllAsync();

        List<NRCType> GetAll();

        Task<NRCType> GetAsync(Guid id);

        Task<NRCType> AddAsync(NRCType model);
        Task<NRCType> UpdateAsync(NRCType model);

        Task<bool> DeleteAsync(Guid id);
    }
}
