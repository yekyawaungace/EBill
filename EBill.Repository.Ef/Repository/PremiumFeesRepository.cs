using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IRepositories;

namespace TravelInsurance.Repository.Ef.Repository
{
    public class PremiumFeesRepository : IPremiumFeesRepository
    {

        #region private
        private readonly ApplicationDbContext _context;
        #endregion
        public PremiumFeesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<PremiumFee>> GetAllAsync()
        {
            return await _context.PremiumFees.ToListAsync();
        }

        public List<PremiumFee> GetAll()
        {
            return _context.PremiumFees.ToList();

            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //}).ToArray();
        }

        public async Task<PremiumFee> GetAsync(Guid id)
        {
            return await _context.PremiumFees.FindAsync(id);
        }

        public async Task<PremiumFee> AddAsync(PremiumFee model)
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

        public async Task<PremiumFee> UpdateAsync(PremiumFee model)
        {
            _context.PremiumFees.Attach(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var model = await _context.PremiumFees.FindAsync(id);
            if (model == null)
                return false;

            _context.Remove(model);
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
