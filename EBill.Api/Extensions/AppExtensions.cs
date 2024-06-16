using TravelInsurance.Infrastructure.IRepositories;
using TravelInsurance.Infrastructure.IServices;
using TravelInsurance.Repository.Ef.Repository;
using TravelInsurance.Service.Services;

namespace TravelInsuranceWebSite.Extensions
{
    public static class AppExtensions
    {
        public static IServiceCollection AddConfig(this IServiceCollection services)
        {
            #region Repository
            services.AddTransient<IStateCodesRepository, StateCodesRepository>();
            services.AddTransient<ITownShipCodeRepository, TownShipCodeRepository>();
            services.AddTransient<INRCTypesRepository, NRCTypesRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IPremiumFeesRepository, PremiumFeesRepository>();
            services.AddTransient<IMerchantsRepository, MerchantsRepository>();
            services.AddTransient<IApplicationsRepository, ApplicationsRepository>();
            services.AddTransient<ITravellersRepository, TravellersRepository>();
            services.AddTransient<ITransactionLogsRepository, TransactionLogsRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<ISettingsRepository, SettingsRepository>();
            #endregion

            #region Service


            services.AddTransient<IStateCodesService, StateCodeService>();
            services.AddTransient<ITownShipCodeService, TownShipCodeService>();
            services.AddTransient<INRCTypesService, NRCTypesService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IPremiumFeesService, PremiumFeesService>();
            services.AddTransient<IMerchantsService, MerchantsService>();
            services.AddTransient<ITravellersService, TravellersService>();
            services.AddTransient<ITransactionLogsService, TransactionLogsService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<ISettingsService, SettingsService>();
            #endregion           

            return services;
        }
    }
}
