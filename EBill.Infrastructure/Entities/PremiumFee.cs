namespace TravelInsurance.Infrastructure.Entities
{
    public class PremiumFee
    {
        public Guid Id { get; set; }
        public int? Duration { get; set; }
        public double? Fee { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedStaffNo { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedStaffNo { get; set; }
    }
}
