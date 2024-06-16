using TravelInsurance.Infrastructure.Entities;

namespace TravelInsurance.Infrastructure.IRepositories
{
    public interface ICountryRepository : IRepository<Country>
    {
        Task<List<Country>> GetAllAsync();

        List<Country> GetAll();

        Task<Country> GetAsync(Guid id);

        Task<Country> AddAsync(Country model);
        Task<Country> UpdateAsync(Country model);

        Task<bool> DeleteAsync(Guid id);
    }
}
