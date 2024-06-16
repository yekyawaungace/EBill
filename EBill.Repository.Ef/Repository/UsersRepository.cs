using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TravelInsurance.Infrastructure.Dto.User;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IRepositories;

namespace TravelInsurance.Repository.Ef.Repository
{
    public class UsersRepository : IUsersRepository
    {

        #region private
        private readonly ApplicationDbContext _context;
        #endregion
        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();

            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //}).ToArray();
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> AddAsync(User model)
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

        public async Task<User> UpdateAsync(User model)
        {
            _context.Users.Attach(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<User> UpdatePasswordAsync(User model)
        {
            _context.Users.Attach(model);
            await _context.SaveChangesAsync();
            return model;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            var model = await _context.Users.FindAsync(id);
            if (model == null)
                return false;

            _context.Remove(model);
            await _context.SaveChangesAsync();

            return true;

        }

        public List<StaffModel> CheckForLogIn(string staff_no, string pwd)
        {
            List<StaffModel> lst = new List<StaffModel>();
            try
            {
                var query = (from staff in _context.Users
                                 // join loc in db.tbl_location on staff.locationID equals loc.LocationID
                             join permi in _context.Permission on staff.UserName equals permi.staffno
                             join role in _context.Roles on permi.roleid equals role.Id
                             where staff.UserName == staff_no && staff.PasswordHash == pwd && staff.IsDeleted == false
                             select new StaffModel
                             {
                                 staffname = staff.UserName,
                             }).ToList();
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
