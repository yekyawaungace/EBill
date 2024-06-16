using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IRepositories;

namespace TravelInsurance.Repository.Ef.Repository
{
    public class TransactionLogsRepository : ITransactionLogsRepository
    {

        #region private
        private readonly ApplicationDbContext _context;
        #endregion
        public TransactionLogsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<TransactionLog>> GetAllAsync()
        {
            return await _context.TransactionLogs.ToListAsync();
        }

        public List<TransactionLog> GetAll()
        {
            return _context.TransactionLogs.ToList();

            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //}).ToArray();
        }

        public async Task<TransactionLog> GetAsync(Guid id)
        {
            return await _context.TransactionLogs.FindAsync(id);
        }

        public async Task<TransactionLog> AddAsync(TransactionLog model)
        {
            try
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString);

                throw;
            }

        }

        public async Task<TransactionLog> UpdateAsync(TransactionLog model)
        {
            _context.TransactionLogs.Attach(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var model = await _context.TransactionLogs.FindAsync(id);
            if (model == null)
                return false;

            _context.Remove(model);
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
