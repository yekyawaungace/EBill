
using TravelInsurance.Infrastructure.Dto.PremiumFees;
using TravelInsurance.Infrastructure.Dto.TownShipCode;

namespace TravelInsurance.Infrastructure.IServices
{
    public interface IPremiumFeesService
    {
        Task<List<PremiumFeesResponseViewModel>> GetAllAsync();
        List<PremiumFeesResponseViewModel> GetAll();
        Task<PremiumFeesResponseViewModel> GetAsync(Guid id);
        Task<bool> AddAsync(PremiumFeesRequestViewModel model);
        Task<bool> UpdateAsync(PremiumFeesRequestViewModel model);
        Task<bool> DeleteAsync(Guid id);
    }
}
