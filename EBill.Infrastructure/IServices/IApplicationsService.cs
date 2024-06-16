
using TravelInsurance.Infrastructure.Dto.Applications;


namespace TravelInsurance.Infrastructure.IServices
{
    public interface IApplicationsService
    {
        Task<List<ApplicationsResponseViewModel>> GetAllAsync();
        List<ApplicationsResponseViewModel> GetAll();
        Task<ApplicationsResponseViewModel> GetAsync(Guid id);
        Task<bool> AddAsync(ApplicationsRequestViewModel model);
        Task<bool> UpdateAsync(ApplicationsRequestViewModel model);
        Task<bool> DeleteAsync(Guid id);
    }
}
