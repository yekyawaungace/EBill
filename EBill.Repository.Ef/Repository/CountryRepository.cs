using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IRepositories;

namespace TravelInsurance.Repository.Ef.Repository
{
    public class CountryRepository : ICountryRepository
    {

        #region private
        private readonly ApplicationDbContext _context;
        #endregion
        public CountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Country>> GetAllAsync()
        {
            return await _context.Country.ToListAsync();
        }

        public List<Country> GetAll()
        {
            return _context.Country.ToList();

            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //}).ToArray();
        }

        public async Task<Country> GetAsync(Guid id)
        {
            return await _context.Country.FindAsync(id);
        }

        public async Task<Country> AddAsync(Country model)
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

        public async Task<Country> UpdateAsync(Country model)
        {
            _context.Country.Attach(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var model = await _context.Country.FindAsync(id);
            if (model == null)
                return false;

            _context.Remove(model);
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
