namespace TravelInsurance.Infrastructure.Dto.User
{
    public class UserRequestViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public Guid RoleId { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedStaffNo { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedStaffNo { get; set; }

    }
}
