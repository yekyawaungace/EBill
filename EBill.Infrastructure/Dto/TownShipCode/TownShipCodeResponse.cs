namespace TravelInsurance.Infrastructure.Dto.TownShipCode
{
    public class TownShipCodeResponse
    {
    }

    public class TownShipCodeResponseViewModel
    {
        public Guid Id { get; set; }
        public Guid StateCodeId { get; set; }
        public string CodeEN { get; set; }
        public string CodeMM { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedStaffNo { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedStaffNo { get; set; }


    }
}
