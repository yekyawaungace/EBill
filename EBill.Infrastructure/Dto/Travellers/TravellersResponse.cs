﻿namespace TravelInsurance.Infrastructure.Dto.Travellers
{
    public class TravellersResponse
    {
    }

    public class TravellersResponseViewModel
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string Name { get; set; }
        public int? NRCType { get; set; }
        public string NRCOrPassportData { get; set; }
        public int? Age { get; set; }


    }
}
