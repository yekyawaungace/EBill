using TravelInsurance.Infrastructure.Dto.StateCode;

namespace TravelInsurance.Infrastructure.IServices
{
    public interface IStateCodesService
    {
        Task<List<StateCodesResponseViewModel>> GetAllAsync();
        List<StateCodesRequestViewModel> GetAll();
        Task<StateCodesResponseViewModel> GetAsync(Guid id);
        Task<bool> AddAsync(StateCodesRequestViewModel model);
        Task<bool> UpdateAsync(StateCodesRequestViewModel model);
        Task<bool> DeleteAsync(Guid id);
    }
}
