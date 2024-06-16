using AutoMapper;
using Microsoft.AspNetCore.Http;
using TravelInsurance.Infrastructure.Dto.Applications;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IRepositories;
using TravelInsurance.Infrastructure.IServices;

namespace TravelInsurance.Service.Services
{
    public class ApplicationsService : IApplicationsService
    {
        #region Private
        private readonly IMapper _mapper;
        private readonly IApplicationsRepository _townShipCodeRepository;
        #endregion
        public ApplicationsService(IApplicationsRepository townShipCodeRepository,
            IMapper mapper)
        {
            _townShipCodeRepository = townShipCodeRepository;
            _mapper = mapper;
        }

        public List<ApplicationsResponseViewModel> GetAll()
        {
            //throw new ArgumentNullException("Dummy Exception Message");
            //throw new AppException("From Application Exception");
            var models = _townShipCodeRepository.GetAll();
            var viewModels = _mapper.Map<List<ApplicationsResponseViewModel>>(models);
            return viewModels;
        }
       

        public async Task<List<ApplicationsResponseViewModel>> GetAllAsync()
        {
            var models = await _townShipCodeRepository.GetAllAsync();
            var viewModels = _mapper.Map<List<ApplicationsResponseViewModel>>(models);
            return viewModels;
        }

        public async Task<ApplicationsResponseViewModel> GetAsync(Guid id)
        {
            var model = await _townShipCodeRepository.GetAsync(id);
            if (model == null)
                return null;
            var viewModel = _mapper.Map<ApplicationsResponseViewModel>(model);
            return viewModel;
        }

        public async Task<bool> UpdateAsync(ApplicationsRequestViewModel model)
        {
            var m = await _townShipCodeRepository.GetAsync(model.Id);
            if (m == null)
                return false;

           
            m.UpdatedDate = DateTime.UtcNow;

            await _townShipCodeRepository.UpdateAsync(m);
            return true;
        }

        public async Task<bool> AddAsync(ApplicationsRequestViewModel viewModel)
        {
            var model = _mapper.Map<Application>(viewModel);
            model.CreatedDate = DateTime.UtcNow;
            model.UpdatedDate = DateTime.UtcNow;
            model.IsActive = true;
            model.IsDeleted = false;
           // model.CreatedStaffNo = HttpContext.Session.GetString("StaffNo");

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
