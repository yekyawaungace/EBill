namespace TravelInsuranceWebSite.Models
{
    public class PermissionModel : MenuInRoleModel
    {
        public PermissionModel()
        {
            permissionid = "";
            staffno = "";
            roleid = "";

            isdeleted = false;
            createddate = new DateTime();
            createdby = "";

            IsCanView = false;
            IsCanAdd = false;
            IsCanUpdate = false;
            IsCanDelete = false;
        }

        public string permissionid { get; set; }
        public string staffno { get; set; }
        public string roleid { get; set; }
        public bool isdeleted { get; set; }
        public DateTime? createddate { get; set; }
        public string createdby { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Updatedstaffno { get; set; }
        public int No { get; set; }
        public int MessageType { get; set; }
        public string Message { get; set; }
    }
}
