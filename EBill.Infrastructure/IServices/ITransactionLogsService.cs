
using TravelInsurance.Infrastructure.Dto.TransactionLogs;

namespace TravelInsurance.Infrastructure.IServices
{
    public interface ITransactionLogsService
    {
        Task<List<TransactionLogsResponseViewModel>> GetAllAsync();
        List<TransactionLogsResponseViewModel> GetAll();
        Task<TransactionLogsResponseViewModel> GetAsync(Guid id);
        Task<bool> AddAsync(TransactionLogsRequestViewModel model);
        Task<bool> UpdateAsync(TransactionLogsRequestViewModel model);
        Task<bool> DeleteAsync(Guid id);
    }
}
