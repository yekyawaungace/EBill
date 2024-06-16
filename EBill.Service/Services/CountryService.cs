using AutoMapper;
using TravelInsurance.Infrastructure.Dto.Country;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IRepositories;
using TravelInsurance.Infrastructure.IServices;

namespace TravelInsurance.Service.Services
{
    public class CountryService : ICountryService
    {
        #region Private
        private readonly IMapper _mapper;
        private readonly ICountryRepository _townShipCodeRepository;
        #endregion
        public CountryService(ICountryRepository townShipCodeRepository,
            IMapper mapper)
        {
            _townShipCodeRepository = townShipCodeRepository;
            _mapper = mapper;
        }

        public List<CountryResponseViewModel> GetAll()
        {
            //throw new ArgumentNullException("Dummy Exception Message");
            //throw new AppException("From Application Exception");
            var models = _townShipCodeRepository.GetAll();
            var viewModels = _mapper.Map<List<CountryResponseViewModel>>(models);
            return viewModels;
        }
       

        public async Task<List<CountryResponseViewModel>> GetAllAsync()
        {
            var models = await _townShipCodeRepository.GetAllAsync();
            var viewModels = _mapper.Map<List<CountryResponseViewModel>>(models);
            return viewModels;
        }

        public async Task<CountryResponseViewModel> GetAsync(Guid id)
        {
            var model = await _townShipCodeRepository.GetAsync(id);
            if (model == null)
                return null;
            var viewModel = _mapper.Map<CountryResponseViewModel>(model);
            return viewModel;
        }

        public async Task<bool> UpdateAsync(CountryRequestViewModel model)
        {
            var m = await _townShipCodeRepository.GetAsync(model.Id);
            if (m == null)
                return false;

          
            m.UpdatedDate = DateTime.UtcNow;

            await _townShipCodeRepository.UpdateAsync(m);
            return true;
        }

        public async Task<bool> AddAsync(CountryRequestViewModel viewModel)
        {
            var model = _mapper.Map<Country>(viewModel);
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
