
using TravelInsurance.Infrastructure.Dto.TownShipCode;

namespace TravelInsurance.Infrastructure.IServices
{
    public interface ITownShipCodeService
    {
        Task<List<TownShipCodeResponseViewModel>> GetAllAsync();
        List<TownShipCodeResponseViewModel> GetAll();
        Task<TownShipCodeResponseViewModel> GetAsync(Guid id);
        Task<bool> AddAsync(TownShipCodeRequestViewModel model);
        Task<bool> UpdateAsync(TownShipCodeRequestViewModel model);
        Task<bool> DeleteAsync(Guid id);
    }
}
