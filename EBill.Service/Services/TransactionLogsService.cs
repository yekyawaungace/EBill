using AutoMapper;
using TravelInsurance.Infrastructure.Dto.TownShipCode;
using TravelInsurance.Infrastructure.Dto.TransactionLogs;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IRepositories;
using TravelInsurance.Infrastructure.IServices;

namespace TravelInsurance.Service.Services
{
    public class TransactionLogsService : ITransactionLogsService
    {
        #region Private
        private readonly IMapper _mapper;
        private readonly ITransactionLogsRepository _townShipCodeRepository;
        #endregion
        public TransactionLogsService(ITransactionLogsRepository townShipCodeRepository,
            IMapper mapper)
        {
            _townShipCodeRepository = townShipCodeRepository;
            _mapper = mapper;
        }

        public List<TransactionLogsResponseViewModel> GetAll()
        {
            //throw new ArgumentNullException("Dummy Exception Message");
            //throw new AppException("From Application Exception");
            var models = _townShipCodeRepository.GetAll();
            var viewModels = _mapper.Map<List<TransactionLogsResponseViewModel>>(models);
            return viewModels;
        }
       

        public async Task<List<TransactionLogsResponseViewModel>> GetAllAsync()
        {
            var models = await _townShipCodeRepository.GetAllAsync();
            var viewModels = _mapper.Map<List<TransactionLogsResponseViewModel>>(models);
            return viewModels;
        }

        public async Task<TransactionLogsResponseViewModel> GetAsync(Guid id)
        {
            var model = await _townShipCodeRepository.GetAsync(id);
            if (model == null)
                return null;
            var viewModel = _mapper.Map<TransactionLogsResponseViewModel>(model);
            return viewModel;
        }

        public async Task<bool> UpdateAsync(TransactionLogsRequestViewModel model)
        {
            var m = await _townShipCodeRepository.GetAsync(model.Id);
            if (m == null)
                return false;

          

            await _townShipCodeRepository.UpdateAsync(m);
            return true;
        }

        public async Task<bool> AddAsync(TransactionLogsRequestViewModel viewModel)
        {
            var model = _mapper.Map<TransactionLog>(viewModel);

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
