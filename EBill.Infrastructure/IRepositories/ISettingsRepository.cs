using TravelInsurance.Infrastructure.Entities;

namespace TravelInsurance.Infrastructure.IRepositories
{
    public interface ISettingsRepository : IRepository<Setting>
    {
        Task<List<Setting>> GetAllAsync();
        List<Setting> GetAll();
        Task<Setting> GetAsync(Guid id);
        Task<Setting> AddAsync(Setting model);
        Task<Setting> UpdateAsync(Setting model);
        Task<Setting> UpdateDesktopBannerAsync(Setting model);
        Task<Setting> UpdateTabletBannerAsync(Setting model);
        Task<Setting> UpdatePopupBannerAsync(Setting model);
        Task<Setting> UpdateMobileBannerAsync(Setting model);
        Task<bool> DeleteAsync(Guid id);
    }
}
