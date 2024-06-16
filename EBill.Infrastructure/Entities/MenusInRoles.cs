namespace TravelInsurance.Infrastructure.Entities
{
    public class MenusInRoles
    {
        public Guid Id { get; set; }
        public Guid RolesID { get; set; }
        public Guid MenuID { get; set; }
        public bool IsCanView { get; set; }
        public bool IsCanAdd { get; set; }
        public bool IsCanUpdate { get; set; }
        public bool IsCanDelete { get; set; }
        public bool? IsCanAdmin { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedStaffNo { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedStaffNo { get; set; }
    }
}
