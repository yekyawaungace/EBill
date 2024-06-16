
using TravelInsurance.Infrastructure.Dto.Country;

namespace TravelInsurance.Infrastructure.IServices
{
    public interface ICountryService
    {
        Task<List<CountryResponseViewModel>> GetAllAsync();
        List<CountryResponseViewModel> GetAll();
        Task<CountryResponseViewModel> GetAsync(Guid id);
        Task<bool> AddAsync(CountryRequestViewModel model);
        Task<bool> UpdateAsync(CountryRequestViewModel model);
        Task<bool> DeleteAsync(Guid id);
    }
}
