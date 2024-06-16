using AutoMapper;
using TravelInsurance.Infrastructure.Dto.Permission;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IRepositories;
using TravelInsurance.Infrastructure.IServices;

namespace TravelInsurance.Service.Services
{
    public class PermissionService : IPermissionService
    {
        #region Private
        private readonly IMapper _mapper;
        private readonly IPermissionRepository _townShipCodeRepository;
        #endregion
        public PermissionService(IPermissionRepository townShipCodeRepository,
            IMapper mapper)
        {
            _townShipCodeRepository = townShipCodeRepository;
            _mapper = mapper;
        }

        public List<PermissionResponseViewModel> GetAll()
        {
            //throw new ArgumentNullException("Dummy Exception Message");
            //throw new AppException("From Application Exception");
            var models = _townShipCodeRepository.GetAll();
            var viewModels = _mapper.Map<List<PermissionResponseViewModel>>(models);
            return viewModels;
        }
       

        public async Task<List<PermissionResponseViewModel>> GetAllAsync()
        {
            var models = await _townShipCodeRepository.GetAllAsync();
            var viewModels = _mapper.Map<List<PermissionResponseViewModel>>(models);
            return viewModels;
        }

        public async Task<PermissionResponseViewModel> GetAsync(Guid id)
        {
            var model = await _townShipCodeRepository.GetAsync(id);
            if (model == null)
                return null;
            var viewModel = _mapper.Map<PermissionResponseViewModel>(model);
            return viewModel;
        }

        public async Task<bool> UpdateAsync(PermissionRequestViewModel model)
        {
            var m = await _townShipCodeRepository.GetAsync(model.Id);
            if (m == null)
                return false;
            await _townShipCodeRepository.UpdateAsync(m);
            return true;
        }

        public async Task<bool> AddAsync(PermissionRequestViewModel viewModel)
        {
            var model = _mapper.Map<Permission>(viewModel);
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
