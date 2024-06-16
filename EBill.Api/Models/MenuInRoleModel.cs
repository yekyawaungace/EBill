namespace TravelInsuranceWebSite.Models
{
    public class MenuInRoleModel
    {
        public MenuInRoleModel()
        {
            MenuInRolesID = 0;
            RolesID = 0;
            MenusID = 0;
            IsCanView = false;
            IsCanAdd = false;
            IsCanUpdate = false;
            IsCanDelete = false;
            CreatedDate = new DateTime();
            CreatedStaffNo = "";
        }

        public int MenuInRolesID { get; set; }
        public int RolesID { get; set; }
        public int MenusID { get; set; }
        public Nullable<bool> IsCanView { get; set; }
        public Nullable<bool> IsCanAdd { get; set; }
        public Nullable<bool> IsCanUpdate { get; set; }
        public Nullable<bool> IsCanDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedStaffNo { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedStaffNo { get; set; }
        public int No { get; set; }
        public int MessageType { get; set; }
        public string Message { get; set; }
    }
}
