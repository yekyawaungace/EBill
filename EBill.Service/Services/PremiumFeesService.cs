using AutoMapper;
using TravelInsurance.Infrastructure.Dto.PremiumFees;
using TravelInsurance.Infrastructure.Dto.TownShipCode;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IRepositories;
using TravelInsurance.Infrastructure.IServices;

namespace TravelInsurance.Service.Services
{
    public class PremiumFeesService : IPremiumFeesService
    {
        #region Private
        private readonly IMapper _mapper;
        private readonly IPremiumFeesRepository _townShipCodeRepository;
        #endregion
        public PremiumFeesService(IPremiumFeesRepository townShipCodeRepository,
            IMapper mapper)
        {
            _townShipCodeRepository = townShipCodeRepository;
            _mapper = mapper;
        }

        public List<PremiumFeesResponseViewModel> GetAll()
        {
            //throw new ArgumentNullException("Dummy Exception Message");
            //throw new AppException("From Application Exception");
            var models = _townShipCodeRepository.GetAll();
            var viewModels = _mapper.Map<List<PremiumFeesResponseViewModel>>(models);
            return viewModels;
        }
       

        public async Task<List<PremiumFeesResponseViewModel>> GetAllAsync()
        {
            var models = await _townShipCodeRepository.GetAllAsync();
            var viewModels = _mapper.Map<List<PremiumFeesResponseViewModel>>(models);
            return viewModels;
        }

        public async Task<PremiumFeesResponseViewModel> GetAsync(Guid id)
        {
            var model = await _townShipCodeRepository.GetAsync(id);
            if (model == null)
                return null;
            var viewModel = _mapper.Map<PremiumFeesResponseViewModel>(model);
            return viewModel;
        }

        public async Task<bool> UpdateAsync(PremiumFeesRequestViewModel model)
        {
            var m = await _townShipCodeRepository.GetAsync(model.Id);
            if (m == null)
                return false;

            m.Duration = model.Duration;
            m.Fee = model.Fee;    
            m.UpdatedDate = DateTime.UtcNow;

            await _townShipCodeRepository.UpdateAsync(m);
            return true;
        }

        public async Task<bool> AddAsync(PremiumFeesRequestViewModel viewModel)
        {
            var model = _mapper.Map<PremiumFee>(viewModel);
            model.CreatedDate = DateTime.UtcNow;
            model.UpdatedDate = DateTime.UtcNow;
            model.IsActive = true;
            model.IsDeleted = false;
            //Guid guid = new Guid();
            //model.CreatedUserID = guid;

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
