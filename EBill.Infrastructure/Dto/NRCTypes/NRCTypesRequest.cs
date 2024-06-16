namespace TravelInsurance.Infrastructure.Dto.NRCTypes
{
    public class NRCTypesRequestViewModel
    {
        public Guid Id { get; set; }
        public string TypeEN { get; set; }
        public string TypeMM { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedStaffNo { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedStaffNo { get; set; }

    }
}
