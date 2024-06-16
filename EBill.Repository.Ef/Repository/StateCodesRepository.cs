using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IRepositories;

namespace TravelInsurance.Repository.Ef.Repository
{
    public class StateCodesRepository : IStateCodesRepository
    {

        #region private
        private readonly ApplicationDbContext _context;
        #endregion
        public StateCodesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<StateCode>> GetAllAsync()
        {
            return await _context.StateCodes.ToListAsync();
        }

        public List<StateCode> GetAll()
        {
            return _context.StateCodes.ToList();

            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //}).ToArray();
        }

        public async Task<StateCode> GetAsync(Guid id)
        {
            return await _context.StateCodes.FindAsync(id);
        }

        public async Task<StateCode> AddAsync(StateCode model)
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

        public async Task<StateCode> UpdateAsync(StateCode model)
        {
            _context.StateCodes.Attach(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var model = await _context.StateCodes.FindAsync(id);
            if (model == null)
                return false;

            _context.Remove(model);
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
