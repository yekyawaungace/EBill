using AutoMapper;
using TravelInsurance.Infrastructure.Dto.StateCode;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IRepositories;
using TravelInsurance.Infrastructure.IServices;

namespace TravelInsurance.Service.Services
{
    public class StateCodeService : IStateCodesService
    {
        #region Private
        private readonly IMapper _mapper;
        private readonly IStateCodesRepository _stateCodeRepository;
        #endregion
        public StateCodeService(IStateCodesRepository stateCodeRepository,
            IMapper mapper)
        {
            _stateCodeRepository = stateCodeRepository;
            _mapper = mapper;
        }

        public List<StateCodesRequestViewModel> GetAll()
        {
            //throw new ArgumentNullException("Dummy Exception Message");
            //throw new AppException("From Application Exception");
            var models = _stateCodeRepository.GetAll();
            var viewModels = _mapper.Map<List<StateCodesRequestViewModel>>(models);
            return viewModels;
        }
       

        public async Task<List<StateCodesResponseViewModel>> GetAllAsync()
        {
            var models = await _stateCodeRepository.GetAllAsync();
            var viewModels = _mapper.Map<List<StateCodesResponseViewModel>>(models);
            return viewModels;
        }

        public async Task<StateCodesResponseViewModel> GetAsync(Guid id)
        {
            var model = await _stateCodeRepository.GetAsync(id);
            if (model == null)
                return null;
            var viewModel = _mapper.Map<StateCodesResponseViewModel>(model);
            return viewModel;
        }

        public async Task<bool> UpdateAsync(StateCodesRequestViewModel model)
        {
            var m = await _stateCodeRepository.GetAsync(model.Id);
            if (m == null)
                return false;
            m.StateCodeEN = model.StateCodeEN;
            m.StateCodeMM = model.StateCodeMM;
            m.UpdatedDate = DateTime.UtcNow;

            await _stateCodeRepository.UpdateAsync(m);
            return true;
        }

        public async Task<bool> AddAsync(StateCodesRequestViewModel viewModel)
        {
           
            var model = _mapper.Map<StateCode>(viewModel);
            model.CreatedDate = DateTime.UtcNow;
            //model.UpdatedDate = DateTime.UtcNow;
            model.IsActive = true;
            model.IsDeleted = false;
            await _stateCodeRepository.AddAsync(model);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            //throw new ArgumentNullException("Exception from Service");
            return await _stateCodeRepository.DeleteAsync(id);
        }
    }
}
