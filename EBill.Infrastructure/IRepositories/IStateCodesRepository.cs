using TravelInsurance.Infrastructure.Entities;

namespace TravelInsurance.Infrastructure.IRepositories
{
    public interface IStateCodesRepository : IRepository<StateCode>
    {
        Task<List<StateCode>> GetAllAsync();

        List<StateCode> GetAll();

        Task<StateCode> GetAsync(Guid id);

        Task<StateCode> AddAsync(StateCode model);
        Task<StateCode> UpdateAsync(StateCode model);

        Task<bool> DeleteAsync(Guid id);
    }
}
