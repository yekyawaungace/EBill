using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IRepositories;

namespace TravelInsurance.Repository.Ef.Repository
{
    public class MerchantsRepository : IMerchantsRepository
    {

        #region private
        private readonly ApplicationDbContext _context;
        #endregion
        public MerchantsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Merchant>> GetAllAsync()
        {
            return await _context.Merchants.ToListAsync();
        }

        public List<Merchant> GetAll()
        {
            return _context.Merchants.ToList();

        }

        public async Task<Merchant> GetAsync(Guid id)
        {
            return await _context.Merchants.FindAsync(id);
        }

        public async Task<Merchant> AddAsync(Merchant model)
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

        public async Task<Merchant> UpdateAsync(Merchant model)
        {
            _context.Merchants.Attach(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteAsync(Guid? id)
        {
            try
            {
                var model = await _context.Merchants.FindAsync(id);
                if (model == null)
                    return false;

                _context.Remove(model);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
         

        }
    }
}
