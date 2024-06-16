using AutoMapper;
using TravelInsurance.Infrastructure.Dto.NRCTypes;
using TravelInsurance.Infrastructure.Dto.TownShipCode;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IRepositories;
using TravelInsurance.Infrastructure.IServices;

namespace TravelInsurance.Service.Services
{
    public class NRCTypesService : INRCTypesService
    {
        #region Private
        private readonly IMapper _mapper;
        private readonly INRCTypesRepository _townShipCodeRepository;
        #endregion
        public NRCTypesService(INRCTypesRepository townShipCodeRepository,
            IMapper mapper)
        {
            _townShipCodeRepository = townShipCodeRepository;
            _mapper = mapper;
        }

        public List<NRCTypesResponseViewModel> GetAll()
        {
            //throw new ArgumentNullException("Dummy Exception Message");
            //throw new AppException("From Application Exception");
            var models = _townShipCodeRepository.GetAll();
            var viewModels = _mapper.Map<List<NRCTypesResponseViewModel>>(models);
            return viewModels;
        }
       

        public async Task<List<NRCTypesResponseViewModel>> GetAllAsync()
        {
            var models = await _townShipCodeRepository.GetAllAsync();
            var viewModels = _mapper.Map<List<NRCTypesResponseViewModel>>(models);
            return viewModels;
        }

        public async Task<NRCTypesResponseViewModel> GetAsync(Guid id)
        {
            var model = await _townShipCodeRepository.GetAsync(id);
            if (model == null)
                return null;
            var viewModel = _mapper.Map<NRCTypesResponseViewModel>(model);
            return viewModel;
        }

        public async Task<bool> UpdateAsync(NRCTypesRequestViewModel model)
        {
            var m = await _townShipCodeRepository.GetAsync(model.Id);
            if (m == null)
                return false;

           
            m.UpdatedDate = DateTime.UtcNow;

            await _townShipCodeRepository.UpdateAsync(m);
            return true;
        }

        public async Task<bool> AddAsync(NRCTypesRequestViewModel viewModel)
        {
            var model = _mapper.Map<NRCType>(viewModel);
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
