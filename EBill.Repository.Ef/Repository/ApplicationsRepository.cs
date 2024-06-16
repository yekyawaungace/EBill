using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IRepositories;

namespace TravelInsurance.Repository.Ef.Repository
{
    public class ApplicationsRepository : IApplicationsRepository
    {

        #region private
        private readonly ApplicationDbContext _context;
        #endregion
        public ApplicationsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Application>> GetAllAsync()
        {
            return await _context.Applications.ToListAsync();
        }

        public List<Application> GetAll()
        {
            return _context.Applications.ToList();

            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //}).ToArray();
        }

        public async Task<Application> GetAsync(Guid id)
        {
            return await _context.Applications.FindAsync(id);
        }

        public async Task<Application> AddAsync(Application model)
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

        public async Task<Application> UpdateAsync(Application model)
        {
            _context.Applications.Attach(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var model = await _context.Applications.FindAsync(id);
            if (model == null)
                return false;

            _context.Remove(model);
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
