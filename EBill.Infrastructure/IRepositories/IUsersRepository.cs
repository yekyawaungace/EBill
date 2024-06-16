using TravelInsurance.Infrastructure.Dto.User;
using TravelInsurance.Infrastructure.Entities;

namespace TravelInsurance.Infrastructure.IRepositories
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<List<User>> GetAllAsync();

        List<User> GetAll();

        Task<User> GetAsync(Guid id);

        Task<User> AddAsync(User model);
        Task<User> UpdateAsync(User model);

        Task<User> UpdatePasswordAsync(User model);

        Task<bool> DeleteAsync(Guid id);

        List<StaffModel> CheckForLogIn(string staff_no, string pwd);
    }
}
