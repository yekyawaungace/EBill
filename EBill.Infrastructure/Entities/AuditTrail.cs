namespace TravelInsurance.Infrastructure.Entities
{
    public class AuditTrail
    {
        public Guid Id { get; set; }
        public Guid ApplicationId { get; set; }
        public DateTime TDateTime { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedStaffNo { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedStaffNo { get; set; }
    }
}
