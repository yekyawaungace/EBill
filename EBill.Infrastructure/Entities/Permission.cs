namespace TravelInsurance.Infrastructure.Entities
{
    public class Permission
    {
        public Guid Id { get; set; }

        public string staffno { get; set; }
        
        public Guid roleid { get; set; }
        public bool isdeleted { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedStaffNo { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedStaffNo { get; set; }


    }
}
