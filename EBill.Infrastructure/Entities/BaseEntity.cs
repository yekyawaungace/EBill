namespace TravelInsurance.Infrastructure.Entities
{ 
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedStaffNo { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedStaffNo { get; set; }
        public bool IsActive { get; set; }

    }
}
