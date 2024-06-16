using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IRepositories;

namespace TravelInsurance.Repository.Ef.Repository
{
    public class TravellersRepository : ITravellersRepository
    {

        #region private
        private readonly ApplicationDbContext _context;
        #endregion
        public TravellersRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Traveller>> GetAllAsync()
        {
            return await _context.Travellers.ToListAsync();
        }

        public List<Traveller> GetAll()
        {
            return _context.Travellers.ToList();

            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //}).ToArray();
        }

        public async Task<Traveller> GetAsync(Guid id)
        {
            return await _context.Travellers.FindAsync(id);
        }

        public async Task<Traveller> AddAsync(Traveller model)
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

        public async Task<Traveller> UpdateAsync(Traveller model)
        {
            _context.Travellers.Attach(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var model = await _context.Travellers.FindAsync(id);
            if (model == null)
                return false;

            _context.Remove(model);
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
