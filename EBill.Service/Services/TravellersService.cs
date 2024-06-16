using AutoMapper;
using TravelInsurance.Infrastructure.Dto.Travellers;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IRepositories;
using TravelInsurance.Infrastructure.IServices;

namespace TravelInsurance.Service.Services
{
    public class TravellersService : ITravellersService
    {
        #region Private
        private readonly IMapper _mapper;
        private readonly ITravellersRepository _townShipCodeRepository;
        #endregion
        public TravellersService(ITravellersRepository townShipCodeRepository,
            IMapper mapper)
        {
            _townShipCodeRepository = townShipCodeRepository;
            _mapper = mapper;
        }

        public List<TravellersResponseViewModel> GetAll()
        {
            //throw new ArgumentNullException("Dummy Exception Message");
            //throw new AppException("From Application Exception");
            var models = _townShipCodeRepository.GetAll();
            var viewModels = _mapper.Map<List<TravellersResponseViewModel>>(models);
            return viewModels;
        }
       

        public async Task<List<TravellersResponseViewModel>> GetAllAsync()
        {
            var models = await _townShipCodeRepository.GetAllAsync();
            var viewModels = _mapper.Map<List<TravellersResponseViewModel>>(models);
            return viewModels;
        }

        public async Task<TravellersResponseViewModel> GetAsync(Guid id)
        {
            var model = await _townShipCodeRepository.GetAsync(id);
            if (model == null)
                return null;
            var viewModel = _mapper.Map<TravellersResponseViewModel>(model);
            return viewModel;
        }

        public async Task<bool> UpdateAsync(TravellersRequestViewModel model)
        {
            var m = await _townShipCodeRepository.GetAsync(model.Id);
            if (m == null)
                return false;
            await _townShipCodeRepository.UpdateAsync(m);
            return true;
        }

        public async Task<bool> AddAsync(TravellersRequestViewModel viewModel)
        {
            var model = _mapper.Map<Traveller>(viewModel);
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
