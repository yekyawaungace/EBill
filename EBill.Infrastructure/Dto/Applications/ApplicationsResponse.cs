namespace TravelInsurance.Infrastructure.Dto.Applications
{
    public class ApplicationsResponse
    {
    }

    public class ApplicationsResponseViewModel
    {
        public Guid Id { get; set; }
        public string OrderNo { get; set; }
        public int NoofPeople { get; set; }
        public string ContactPhone { get; set; }
        public int? OverSeaOrDomestic { get; set; }
        public string FromDestination { get; set; }
        public string ToDestination { get; set; }
        public double? PremiumFee { get; set; }
        public string TransitionID { get; set; }
        public string Address { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SecondContactPerson { get; set; }
        public string SecondContactPhone { get; set; }
        public int PaymentStatus { get; set; }
        public int Endosement { get; set; }
        public string CertificateID { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedStaffNo { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedStaffNo { get; set; }


    }
}
