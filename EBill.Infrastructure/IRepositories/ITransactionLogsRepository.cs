using TravelInsurance.Infrastructure.Entities;

namespace TravelInsurance.Infrastructure.IRepositories
{
    public interface ITransactionLogsRepository : IRepository<TransactionLog>
    {
        Task<List<TransactionLog>> GetAllAsync();

        List<TransactionLog> GetAll();

        Task<TransactionLog> GetAsync(Guid id);

        Task<TransactionLog> AddAsync(TransactionLog model);
        Task<TransactionLog> UpdateAsync(TransactionLog model);

        Task<bool> DeleteAsync(Guid id);
    }
}
