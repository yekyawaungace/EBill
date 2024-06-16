
using TravelInsurance.Infrastructure.Dto.Travellers;


namespace TravelInsurance.Infrastructure.IServices
{
    public interface ITravellersService
    {
        Task<List<TravellersResponseViewModel>> GetAllAsync();
        List<TravellersResponseViewModel> GetAll();
        Task<TravellersResponseViewModel> GetAsync(Guid id);
        Task<bool> AddAsync(TravellersRequestViewModel model);
        Task<bool> UpdateAsync(TravellersRequestViewModel model);
        Task<bool> DeleteAsync(Guid id);
    }
}
