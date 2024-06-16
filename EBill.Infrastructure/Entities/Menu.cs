namespace TravelInsurance.Infrastructure.Entities
{
    public class Menu
    {
        public Guid Id { get; set; }
        public string MenuName { get; set; }
        public string MenuNameTrim { get; set; }
        public Guid? ParentMenuID { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedStaffNo { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedStaffNo { get; set; }
    }
}
