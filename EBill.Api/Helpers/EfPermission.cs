using Microsoft.EntityFrameworkCore;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Repository.Ef;
using TravelInsuranceWebSite.Controllers;
using TravelInsuranceWebSite.Models;

namespace TravelInsuranceWebSite.Helpers
{
    public class EfPermission
    {
        //#region Declaration
        ApplicationDbContext db = new ApplicationDbContext();
      /*  private readonly ApplicationDbContext db;
        #endregion

        public EfPermission(ApplicationDbContext context)
        {
            
            db = context;
        }*/

        #region Save
        public int Save(Permission otbl_permission)
        {
            int isSuccess = 0;
            try
            {
                db.Permission.Add(otbl_permission);
                isSuccess = db.SaveChanges();
            }
            catch { }
            return isSuccess;
        }
        #endregion

        #region CheckExist
        public int CheckExist(string StaffNo)
        {
            int count = 0;

            try
            {
                var query = (from per in db.Permission
                             join role in db.Roles on per.roleid equals role.Id
                             where per.staffno == StaffNo && per.isdeleted == false && role.IsDeleted == false
                             select per).ToList();
                count = query.Count;
            }
            catch { }

            return count;
        }
        #endregion

        #region DeleteByStaffNo
        public int DeleteByStaffNo(string StaffNo)
        {
            int isSuccess = 0;
            try
            {
                foreach (Permission perm in db.Permission.Where(x => x.staffno == StaffNo))
                {
                    db.Permission.Remove(perm);
                }
                isSuccess = db.SaveChanges();
            }
            catch { }
            return isSuccess;
        }
        #endregion

        #region SelectDataByMenuID
        public List<PermissionModel> SelectDataByMenuID(Guid MenuID)
        {
            List<PermissionModel> lst = new List<PermissionModel>();
            try
            {
                lst = (from perm in db.Permission
                       join mroles in db.MenusInRoles on perm.roleid equals mroles.RolesID
                       where mroles.MenuID == MenuID
                       select new PermissionModel
                       {
                           IsCanView = mroles.IsCanView,
                           IsCanAdd = mroles.IsCanAdd,
                           IsCanUpdate = mroles.IsCanUpdate,
                           IsCanDelete = mroles.IsCanDelete
                       }).ToList();
            }
            catch { }
            return lst;
        }
        #endregion

        public string SelectRoleNameByStaffNo(string staffno)
        {
            string rolename = "";
            try
            {
                rolename = (from perm in db.Permission
                            join role in db.Roles on perm.roleid equals role.Id
                            where perm.staffno == staffno
                            select role.RoleName).FirstOrDefault();

            }
            catch { }
            return rolename;
        }
    }
}
