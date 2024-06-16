using AutoMapper;
using TravelInsurance.Infrastructure.Dto.StateCode;
using TravelInsurance.Infrastructure.Dto.User;
using TravelInsurance.Infrastructure.Entities;

namespace TravelInsurance.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           
            CreateMap<StateCode, Infrastructure.Dto.StateCode.StateCodesResponseViewModel>();
            CreateMap<Infrastructure.Dto.StateCode.StateCodesRequestViewModel, StateCode>();
            CreateMap<StateCode, Infrastructure.Dto.StateCode.StateCodesRequestViewModel>();
            CreateMap<StateCodeRequest, Infrastructure.Dto.StateCode.StateCodesRequestViewModel>();
            CreateMap<Infrastructure.Dto.StateCode.StateCodesRequestViewModel,StateCodeRequest>();
            CreateMap<StateCodeRequest, StateCode>();
            CreateMap<StateCode, StateCodeRequest>();



            CreateMap<TownShipCode, Infrastructure.Dto.TownShipCode.TownShipCodeResponseViewModel>();
            CreateMap<Infrastructure.Dto.TownShipCode.TownShipCodeRequestViewModel, TownShipCode>();
            CreateMap<TownShipCode, Infrastructure.Dto.TownShipCode.TownShipCodeRequestViewModel>();


            CreateMap<NRCType, Infrastructure.Dto.NRCTypes.NRCTypesResponseViewModel>();
            CreateMap<Infrastructure.Dto.NRCTypes.NRCTypesRequestViewModel, NRCType>();

            CreateMap<Country, Infrastructure.Dto.Country.CountryResponseViewModel>();
            CreateMap<Infrastructure.Dto.Country.CountryRequestViewModel, Country>();

            CreateMap<PremiumFee, Infrastructure.Dto.PremiumFees.PremiumFeesResponseViewModel>();
            CreateMap<Infrastructure.Dto.PremiumFees.PremiumFeesRequestViewModel, PremiumFee>();
            CreateMap<PremiumFee, Infrastructure.Dto.PremiumFees.PremiumFeesRequestViewModel>();

            CreateMap<Merchant, Infrastructure.Dto.Merchants.MerchantsResponseViewModel>();
            CreateMap<Infrastructure.Dto.Merchants.MerchantsRequestViewModel, Merchant>();
            CreateMap<Merchant,Infrastructure.Dto.Merchants.MerchantsRequestViewModel>();

            CreateMap<Application, Infrastructure.Dto.Applications.ApplicationsResponseViewModel>();
            CreateMap<Infrastructure.Dto.Applications.ApplicationsRequestViewModel, Application>();

            CreateMap<Traveller, Infrastructure.Dto.Travellers.TravellersResponseViewModel>();
            CreateMap<Infrastructure.Dto.Travellers.TravellersRequestViewModel, Traveller>();

            CreateMap<TransactionLog, Infrastructure.Dto.TransactionLogs.TransactionLogsResponseViewModel>();
            CreateMap<Infrastructure.Dto.TransactionLogs.TransactionLogsRequestViewModel, TransactionLog>();

            CreateMap<User, Infrastructure.Dto.User.UserResponseViewModel>();
            CreateMap<Infrastructure.Dto.User.UserRequestViewModel, User>();
            CreateMap<User, Infrastructure.Dto.User.UserRequestViewModel>();

            CreateMap<Permission, Infrastructure.Dto.Permission.PermissionResponseViewModel>();
            CreateMap<Infrastructure.Dto.Permission.PermissionRequestViewModel, Permission>();

            CreateMap<Setting, Infrastructure.Dto.Settings.SettingsResponseViewModel>();
            CreateMap<Infrastructure.Dto.Settings.SettingsRequestViewModel, Setting>();
            CreateMap<Setting, Infrastructure.Dto.Settings.SettingsRequestViewModel>();
        }
    }
}
