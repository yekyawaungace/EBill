using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IRepositories;

namespace TravelInsurance.Repository.Ef.Repository
{
    public class TownShipCodeRepository : ITownShipCodeRepository
    {

        #region private
        private readonly ApplicationDbContext _context;
        #endregion
        public TownShipCodeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<TownShipCode>> GetAllAsync()
        {
            return await _context.TownShipCodes.ToListAsync();
        }

        public List<TownShipCode> GetAll()
        {
            return _context.TownShipCodes.ToList();

            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //}).ToArray();
        }

        public async Task<TownShipCode> GetAsync(Guid id)
        {
            return await _context.TownShipCodes.FindAsync(id);
        }

        public async Task<TownShipCode> AddAsync(TownShipCode model)
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

        public async Task<TownShipCode> UpdateAsync(TownShipCode model)
        {
            _context.TownShipCodes.Attach(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var model = await _context.TownShipCodes.FindAsync(id);
            if (model == null)
                return false;

            _context.Remove(model);
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
