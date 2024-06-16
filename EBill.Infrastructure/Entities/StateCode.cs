namespace TravelInsurance.Infrastructure.Entities
{
    public class StateCode
    {
        public Guid Id { get; set; }
        public string StateCodeEN { get; set; }
        public string StateCodeMM { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedStaffNo { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedStaffNo { get; set; }
    }
}