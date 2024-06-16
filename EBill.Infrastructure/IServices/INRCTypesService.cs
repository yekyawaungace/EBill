
using TravelInsurance.Infrastructure.Dto.NRCTypes;

namespace TravelInsurance.Infrastructure.IServices
{
    public interface INRCTypesService
    {
        Task<List<NRCTypesResponseViewModel>> GetAllAsync();
        List<NRCTypesResponseViewModel> GetAll();
        Task<NRCTypesResponseViewModel> GetAsync(Guid id);
        Task<bool> AddAsync(NRCTypesRequestViewModel model);
        Task<bool> UpdateAsync(NRCTypesRequestViewModel model);
        Task<bool> DeleteAsync(Guid id);
    }
}
