using Microsoft.EntityFrameworkCore;
using TravelInsurance.Infrastructure.IServices;
using TravelInsurance.Repository.Ef;
using TravelInsuranceWebSite.Controllers;
using TravelInsuranceWebSite.Models;

namespace TravelInsuranceWebSite.Helpers
{
    public class EfStaff
    {

        #region Declaration
        //ApplicationDbContext db = new ApplicationDbContext();
        private readonly ApplicationDbContext db;
        #endregion


        public EfStaff(ApplicationDbContext context)
        {
            db = context;
        }


        #region CheckForLogIn
        //TMN
        public List<StaffModel> CheckForLogIn(string staff_no, string pwd)
        {
            List<StaffModel> lst = new List<StaffModel>();
            try
            {
                var query = (from staff in db.Users
                            // join loc in db.tbl_location on staff.locationID equals loc.LocationID
                             join permi in db.Permission on staff.UserName equals permi.staffno
                             join role in db.Roles on permi.roleid equals role.Id
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
        #endregion

    
    }
}
