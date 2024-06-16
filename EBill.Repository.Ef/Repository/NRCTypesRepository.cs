using Microsoft.EntityFrameworkCore;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IRepositories;

namespace TravelInsurance.Repository.Ef.Repository
{
    public class NRCTypesRepository : INRCTypesRepository
    {

        #region private
        private readonly ApplicationDbContext _context;
        #endregion
        public NRCTypesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<NRCType>> GetAllAsync()
        {
            return await _context.NRCTypes.ToListAsync();
        }

        public List<NRCType> GetAll()
        {
            return _context.NRCTypes.ToList();

            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //}).ToArray();
        }

        public async Task<NRCType> GetAsync(Guid id)
        {
            return await _context.NRCTypes.FindAsync(id);
        }

        public async Task<NRCType> AddAsync(NRCType model)
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

        public async Task<NRCType> UpdateAsync(NRCType model)
        {
            _context.NRCTypes.Attach(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var model = await _context.NRCTypes.FindAsync(id);
            if (model == null)
                return false;

            _context.Remove(model);
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
