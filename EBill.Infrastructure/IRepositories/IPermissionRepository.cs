using TravelInsurance.Infrastructure.Entities;

namespace TravelInsurance.Infrastructure.IRepositories
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        Task<List<Permission>> GetAllAsync();

        List<Permission> GetAll();

        Task<Permission> GetAsync(Guid id);

        Task<Permission> AddAsync(Permission model);
        Task<Permission> UpdateAsync(Permission model);

        Task<bool> DeleteAsync(Guid id);
    }
}
