namespace TravelInsurance.Infrastructure.Dto.Settings
{
    public class SettingsRequestViewModel
    {
        public Guid Id { get; set; }
        public string Banner { get; set; }
        public string TabletBanner { get; set; }
        public string MobileBanner { get; set; }
        public string? PopupBanner { get; set; }
        public string Body { get; set; }
        public string? EnglishBody { get; set; }
        public string? ContactUs { get; set; }
        public string? WebSite { get; set; }
        public string? FaceBook { get; set; }
        public string? LinkedIn { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedStaffNo { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedStaffNo { get; set; }

    }
}
