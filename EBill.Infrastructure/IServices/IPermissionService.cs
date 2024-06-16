
using TravelInsurance.Infrastructure.Dto.Permission;


namespace TravelInsurance.Infrastructure.IServices
{
    public interface IPermissionService
    {
        Task<List<PermissionResponseViewModel>> GetAllAsync();
        List<PermissionResponseViewModel> GetAll();
        Task<PermissionResponseViewModel> GetAsync(Guid id);
        Task<bool> AddAsync(PermissionRequestViewModel model);
        Task<bool> UpdateAsync(PermissionRequestViewModel model);
        Task<bool> DeleteAsync(Guid id);
    }
}
