using AutoMapper;
using TravelInsurance.Infrastructure.Dto.TownShipCode;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IRepositories;
using TravelInsurance.Infrastructure.IServices;

namespace TravelInsurance.Service.Services
{
    public class TownShipCodeService : ITownShipCodeService
    {
        #region Private
        private readonly IMapper _mapper;
        private readonly ITownShipCodeRepository _townShipCodeRepository;
        #endregion
        public TownShipCodeService(ITownShipCodeRepository townShipCodeRepository,
            IMapper mapper)
        {
            _townShipCodeRepository = townShipCodeRepository;
            _mapper = mapper;
        }

        public List<TownShipCodeResponseViewModel> GetAll()
        {
            //throw new ArgumentNullException("Dummy Exception Message");
            //throw new AppException("From Application Exception");
            var models = _townShipCodeRepository.GetAll();
            var viewModels = _mapper.Map<List<TownShipCodeResponseViewModel>>(models);
            return viewModels;
        }
       

        public async Task<List<TownShipCodeResponseViewModel>> GetAllAsync()
        {
            var models = await _townShipCodeRepository.GetAllAsync();
            var viewModels = _mapper.Map<List<TownShipCodeResponseViewModel>>(models);
            return viewModels;
        }

        public async Task<TownShipCodeResponseViewModel> GetAsync(Guid id)
        {
            var model = await _townShipCodeRepository.GetAsync(id);
            if (model == null)
                return null;
            var viewModel = _mapper.Map<TownShipCodeResponseViewModel>(model);
            return viewModel;
        }

        public async Task<bool> UpdateAsync(TownShipCodeRequestViewModel model)
        {
            var m = await _townShipCodeRepository.GetAsync(model.Id);
            if (m == null)
                return false;

            m.StateCodeId = model.StateCodeId;
            m.CodeEN = model.CodeEN;
            m.CodeMM = model.CodeMM;    
            m.UpdatedDate = DateTime.UtcNow;

            await _townShipCodeRepository.UpdateAsync(m);
            return true;
        }

        public async Task<bool> AddAsync(TownShipCodeRequestViewModel viewModel)
        {
            var model = _mapper.Map<TownShipCode>(viewModel);
            model.CreatedDate = DateTime.Now;
            model.IsActive = true;
            model.IsDeleted = false;

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
