namespace TravelInsurance.Infrastructure.Dto.Country
{
    public class CountryResponse
    {
    }

    public class CountryResponseViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedStaffNo { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedStaffNo { get; set; }


    }
}
