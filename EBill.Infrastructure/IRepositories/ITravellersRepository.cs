using TravelInsurance.Infrastructure.Entities;

namespace TravelInsurance.Infrastructure.IRepositories
{
    public interface ITravellersRepository : IRepository<Traveller>
    {
        Task<List<Traveller>> GetAllAsync();

        List<Traveller> GetAll();

        Task<Traveller> GetAsync(Guid id);

        Task<Traveller> AddAsync(Traveller model);
        Task<Traveller> UpdateAsync(Traveller model);

        Task<bool> DeleteAsync(Guid id);
    }
}
