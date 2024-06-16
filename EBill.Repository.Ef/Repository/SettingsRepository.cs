using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IRepositories;

namespace TravelInsurance.Repository.Ef.Repository
{
    public class SettingsRepository : ISettingsRepository
    {

        #region private
        private readonly ApplicationDbContext _context;
        #endregion
        public SettingsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Setting>> GetAllAsync()
        {
            return await _context.Settings.ToListAsync();
        }

        public List<Setting> GetAll()
        {
            return _context.Settings.ToList();

            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //}).ToArray();
        }

        public async Task<Setting> GetAsync(Guid id)
        {
            return await _context.Settings.FindAsync(id);
        }

        public async Task<Setting> AddAsync(Setting model)
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

        public async Task<Setting> UpdateAsync(Setting model)
        {
            _context.Settings.Attach(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Setting> UpdateDesktopBannerAsync(Setting model)
        {
            _context.Settings.Attach(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Setting> UpdateTabletBannerAsync(Setting model)
        {
            _context.Settings.Attach(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Setting> UpdatePopupBannerAsync(Setting model)
        {
            _context.Settings.Attach(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Setting> UpdateMobileBannerAsync(Setting model)
        {
            _context.Settings.Attach(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var model = await _context.Settings.FindAsync(id);
            if (model == null)
                return false;

            _context.Remove(model);
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
