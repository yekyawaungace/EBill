
using TravelInsurance.Infrastructure.Dto.Settings;

namespace TravelInsurance.Infrastructure.IServices
{
    public interface ISettingsService
    {
        Task<List<SettingsResponseViewModel>> GetAllAsync();
        List<SettingsResponseViewModel> GetAll();
        Task<SettingsResponseViewModel> GetAsync(Guid id);
        Task<bool> AddAsync(SettingsRequestViewModel model);
        Task<bool> UpdateAsync(SettingsRequestViewModel model);

        Task<bool> UpdateDesktopBannerAsync(SettingsRequestViewModel model);
        Task<bool> UpdateTabletBannerAsync(SettingsRequestViewModel model);
        Task<bool> UpdatePopupBannerAsync(SettingsRequestViewModel model);
        Task<bool> UpdateMobileBannerAsync(SettingsRequestViewModel model);
        Task<bool> DeleteAsync(Guid id);
    }
}
