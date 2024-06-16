namespace TravelInsuranceWebSite.Models
{
    public class MenuModel
    {
        public MenuModel()
        {
            menuid = Guid.NewGuid();
            menuname = "";
            parentmenuname = "";
            programpath = "";
            parentmenuid = Guid.NewGuid();
            icon = "";
            sortorder = 0;
            isdeleted = false;
            createddate = new DateTime();
            messagetype = 0;
            message = "";
        }




        public Guid menuid { get; set; }
        public string menuname { get; set; }
        public string parentmenuname { get; set; }
        public string programpath { get; set; }
        public Guid? parentmenuid { get; set; }
        public string icon { get; set; }
        public int sortorder { get; set; }
        public bool isdeleted { get; set; }
        public byte[] ts { get; set; }
        public System.DateTime createddate { get; set; }
        public string createdby { get; set; }
        public Nullable<System.DateTime> modifieddate { get; set; }
        public string modifiedby { get; set; }

        public int sr { get; set; }
        public int messagetype { get; set; }
        public string message { get; set; }
    }
}
