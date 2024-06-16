
using TravelInsurance.Infrastructure.Dto.User;

namespace TravelInsurance.Infrastructure.IServices
{
    public interface IUsersService
    {
        Task<List<UserResponseViewModel>> GetAllAsync();
        List<UserResponseViewModel> GetAll();
        Task<UserResponseViewModel> GetAsync(Guid id);
        Task<bool> AddAsync(UserRequestViewModel model);
        Task<bool> UpdateAsync(UserRequestViewModel model);

        Task<bool> UpdatePasswordAsync(UserRequestViewModel model);
        Task<bool> DeleteAsync(Guid id);
        List<StaffModel> CheckForLogIn(string staff_no, string pwd);
    }
}
