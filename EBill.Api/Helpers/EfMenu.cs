using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Repository.Ef;
using TravelInsuranceWebSite.Models;

namespace TravelInsuranceWebSite.Helpers
{
    public class EfMenu
    {

        #region Declaration
        ApplicationDbContext db = new ApplicationDbContext();
        #endregion

        #region SelectAllData
        public List<MenuModel> SelectAllData(int startIndex, int count, string sorting)
        {
            IEnumerable<MenuModel> query = (from pmenu in db.Menus
                                            join menu in db.Menus on pmenu.ParentMenuID equals menu.Id into lmenu
                                            from lmenudata in lmenu.DefaultIfEmpty()
                                            where pmenu.IsDeleted == false
                                            select new MenuModel
                                            {
                                                menuid = pmenu.Id,
                                                menuname = pmenu.MenuName,
                                               /* parentmenuname = lmenudata.menuname,
                                                programpath = pmenu.programpath*/
                                            });
            query = query.AsEnumerable().Select((menu, index) => new MenuModel()
            {
                menuid = menu.menuid,
                menuname = menu.menuname,
                parentmenuname = menu.parentmenuname,
                programpath = menu.programpath,
                sr = ++index
            });
            return count > 0
                       ? query.Skip(startIndex).Take(count).ToList()
                       : query.ToList();
        }
        #endregion

        #region BindPermissionMenu
        public List<MenuModel> BindPermissionMenu()
        {
            List<MenuModel> lst = new List<MenuModel>();
            try
            {
                var query = (from menu in db.Menus
                             select new MenuModel
                             {
                                 menuid = menu.Id,
                                 menuname = menu.MenuName,
                                 parentmenuid = menu.ParentMenuID
                                 //programpath = menu.P
                             }).ToList();

                return query;

            }
            catch { }
            return lst;
        }
        #endregion

        #region BindMenu
        public List<MenuModel> BindMenu()
        {
            List<MenuModel> lst = new List<MenuModel>();
            try
            {
                var query = (from menu in db.Menus
                             where menu.IsDeleted == false
                             //orderby menu.sortorder
                             select new MenuModel
                             {
                                 menuid = menu.Id,
                                 menuname = menu.MenuName,
                                 parentmenuid = menu.ParentMenuID,
                                 //programpath = menu.programpath,
                                // icon = menu.icon
                             }).ToList();

                return query;

            }
            catch { }
            return lst;
        }
        #endregion

        #region MenusCount
        public int MenusCount()
        {
            int count = 0;
            try
            {
                var query = (from pmenu in db.Menus
                             join menu in db.Menus on pmenu.ParentMenuID equals menu.Id into lmenu
                             from lmenudata in lmenu.DefaultIfEmpty()
                             where pmenu.IsDeleted == false
                             select new MenuModel
                             {
                                 menuid = pmenu.Id,
                                 menuname = pmenu.MenuName,
                                 //parentmenuname = lmenudata,
                                 //programpath = pmenu.programpath
                             });
                count = query.Count();
            }
            catch { }
            return count;
        }
        #endregion

        #region Save
        public int Save(Menu oMenu)
        {
            int isSuccess = 0;
            try
            {
                db.Menus.Add(oMenu);
                isSuccess = db.SaveChanges();
            }
            catch { }
            return isSuccess;
        }
        #endregion

        #region Update
        public int Update(Menu oMenu)
        {
            int isSuccess = 0;
            try
            {
                db.Menus.First(x => x.Id == oMenu.Id).MenuName = oMenu.MenuName;
                db.Menus.First(x => x.Id == oMenu.Id).ParentMenuID = oMenu.ParentMenuID;
                //db.Menus.First(x => x.menuid == oMenu.menuid).programpath = oMenu.programpath;

                isSuccess = db.SaveChanges();
            }
            catch { }
            return isSuccess;
        }
        #endregion

        #region Delete
        public int Delete(Guid menuid)
        {
            int isSuccess = 0;
            try
            {
                db.Menus.First(x => x.Id == menuid).IsDeleted = true;

                isSuccess = db.SaveChanges();
            }
            catch { }
            return isSuccess;
        }
        #endregion

        #region SelectDataByID
        public MenuModel SelectDataByID(Guid id)
        {
            MenuModel oMenuModel = new MenuModel();

            try
            {
                oMenuModel = (from menu in db.Menus
                              where menu.Id == id
                              select new MenuModel
                              {
                                  menuid = menu.Id,
                                  menuname = menu.MenuName,
                                 /* parentmenuid = menu.parentmenuid,
                                  programpath = menu.programpath*/
                              }).First();

            }
            catch { }
            return oMenuModel;
        }
        #endregion

        #region CheckExist
        public int CheckExist(Menu oMenu)
        {
            int count = 0;
            try
            {
                var query = (from menu in db.Menus
                             where menu.MenuName == oMenu.MenuName
                                 && menu.IsDeleted == false
                             select menu).ToList();
                count = query.Count;
            }
            catch { }

            return count;
        }
        #endregion

        #region CheckExistForUpdate
        public int CheckExistForUpdate(Menu oMenu)
        {
            int count = 0;
            try
            {
                var query = (from menu in db.Menus where menu.MenuName == oMenu.MenuName && menu.Id != oMenu.Id && menu.IsDeleted == false select menu).ToList();
                count = query.Count;
            }
            catch { }

            return count;
        }
        #endregion

        #region getmenuidByprogrampath
      /*  public string getmenuidByprogrampath(string path)
        {
            string menu_id = "";
            try
            {
                var query = (from menu in db.Menus where menu.programpath == path select menu).First();
                menu_id = query.menuid;
            }
            catch { }
            return menu_id;
        }*/
        #endregion


    }
}
