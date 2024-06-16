using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IRepositories;

namespace TravelInsurance.Repository.Ef.Repository
{
    public class PermissionRepository : IPermissionRepository
    {

        #region private
        private readonly ApplicationDbContext _context;
        #endregion
        public PermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Permission>> GetAllAsync()
        {
            return await _context.Permission.ToListAsync();
        }

        public List<Permission> GetAll()
        {
            return _context.Permission.ToList();

            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //}).ToArray();
        }

        public async Task<Permission> GetAsync(Guid id)
        {
            return await _context.Permission.FindAsync(id);
        }

        public async Task<Permission> AddAsync(Permission model)
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

        public async Task<Permission> UpdateAsync(Permission model)
        {
            _context.Permission.Attach(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var model = await _context.Permission.FindAsync(id);
            if (model == null)
                return false;

            _context.Remove(model);
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
