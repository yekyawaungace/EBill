using TravelInsurance.Infrastructure.Entities;

namespace TravelInsurance.Infrastructure.IRepositories
{
    public interface IApplicationsRepository : IRepository<Application>
    {
        Task<List<Application>> GetAllAsync();

        List<Application> GetAll();

        Task<Application> GetAsync(Guid id);

        Task<Application> AddAsync(Application model);
        Task<Application> UpdateAsync(Application model);

        Task<bool> DeleteAsync(Guid id);
    }
}
