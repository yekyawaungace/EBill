﻿namespace TravelInsurance.Infrastructure.Dto.Merchants
{
    public class MerchantsResponse
    {
    }

    public class MerchantsResponseViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string QRCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedStaffNo { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedStaffNo { get; set; }


    }
}
