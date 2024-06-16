using AutoMapper;
using TravelInsurance.Infrastructure.Dto.Settings;
using TravelInsurance.Infrastructure.Dto.TownShipCode;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IRepositories;
using TravelInsurance.Infrastructure.IServices;

namespace TravelInsurance.Service.Services
{
    public class SettingsService : ISettingsService
    {
        #region Private
        private readonly IMapper _mapper;
        private readonly ISettingsRepository _townShipCodeRepository;
        #endregion
        public SettingsService(ISettingsRepository townShipCodeRepository,
            IMapper mapper)
        {
            _townShipCodeRepository = townShipCodeRepository;
            _mapper = mapper;
        }

        public List<SettingsResponseViewModel> GetAll()
        {
            //throw new ArgumentNullException("Dummy Exception Message");
            //throw new AppException("From Application Exception");
            var models = _townShipCodeRepository.GetAll();
            var viewModels = _mapper.Map<List<SettingsResponseViewModel>>(models);
            return viewModels;
        }
       

        public async Task<List<SettingsResponseViewModel>> GetAllAsync()
        {
            var models = await _townShipCodeRepository.GetAllAsync();
            var viewModels = _mapper.Map<List<SettingsResponseViewModel>>(models);
            return viewModels;
        }

        public async Task<SettingsResponseViewModel> GetAsync(Guid id)
        {
            var model = await _townShipCodeRepository.GetAsync(id);
            if (model == null)
                return null;
            var viewModel = _mapper.Map<SettingsResponseViewModel>(model);
            return viewModel;
        }

        public async Task<bool> UpdateAsync(SettingsRequestViewModel model)
        {
            var m = await _townShipCodeRepository.GetAsync(model.Id);
            if (m == null)
                return false;

            m.Body = model.Body;
            m.EnglishBody = model.EnglishBody;
            m.UpdatedDate = DateTime.UtcNow;

            await _townShipCodeRepository.UpdateAsync(m);
            return true;
        }

        public async Task<bool> UpdateDesktopBannerAsync(SettingsRequestViewModel model)
        {
            var m = await _townShipCodeRepository.GetAsync(model.Id);
            if (m == null)
                return false;

            m.Banner = model.Banner;
            m.UpdatedDate = DateTime.UtcNow;

            await _townShipCodeRepository.UpdateAsync(m);
            return true;
        }

        public async Task<bool> UpdateTabletBannerAsync(SettingsRequestViewModel model)
        {
            var m = await _townShipCodeRepository.GetAsync(model.Id);
            if (m == null)
                return false;

            m.TabletBanner = model.TabletBanner;
            m.UpdatedDate = DateTime.UtcNow;

            await _townShipCodeRepository.UpdateAsync(m);
            return true;
        }
        public async Task<bool> UpdatePopupBannerAsync(SettingsRequestViewModel model)
        {
            var m = await _townShipCodeRepository.GetAsync(model.Id);
            if (m == null)
                return false;

            m.PopupBanner = model.PopupBanner;
            m.UpdatedDate = DateTime.UtcNow;

            await _townShipCodeRepository.UpdateAsync(m);
            return true;
        }

        public async Task<bool> UpdateMobileBannerAsync(SettingsRequestViewModel model)
        {
            var m = await _townShipCodeRepository.GetAsync(model.Id);
            if (m == null)
                return false;

            m.MobileBanner = model.MobileBanner;
            m.UpdatedDate = DateTime.UtcNow;

            await _townShipCodeRepository.UpdateAsync(m);
            return true;
        }

        public async Task<bool> AddAsync(SettingsRequestViewModel viewModel)
        {
            var model = _mapper.Map<Setting>(viewModel);
            model.CreatedDate = DateTime.UtcNow;
            model.UpdatedDate = DateTime.UtcNow;
            model.IsActive = true;
            model.IsDeleted = false;
            /*Guid guid = new Guid();
            model.CreatedUserID = guid;*/

            await _townShipCodeRepository.AddAsync(model);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            //throw new ArgumentNullException("Exception from Service");
            return await _townShipCodeRepository.DeleteAsync(id);
        }
    }
}
