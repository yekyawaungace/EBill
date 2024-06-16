
using TravelInsurance.Infrastructure.Dto.Merchants;


namespace TravelInsurance.Infrastructure.IServices
{
    public interface IMerchantsService
    {
        Task<List<MerchantsResponseViewModel>> GetAllAsync();
        List<MerchantsResponseViewModel> GetAll();
        Task<MerchantsResponseViewModel> GetAsync(Guid id);
        Task<bool> AddAsync(MerchantsRequestViewModel model);
        Task<bool> UpdateAsync(MerchantsRequestViewModel model);
        Task<bool> DeleteAsync(Guid? id);
    }
}
