using AutoMapper;
using Microsoft.AspNetCore.Http;
using TravelInsurance.Infrastructure.Dto.Merchants;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IRepositories;
using TravelInsurance.Infrastructure.IServices;

namespace TravelInsurance.Service.Services
{
    public class MerchantsService : IMerchantsService
    {
        #region Private
        private readonly IMapper _mapper;
        private readonly IMerchantsRepository _townShipCodeRepository;
        #endregion
        public MerchantsService(IMerchantsRepository townShipCodeRepository,
         
            IMapper mapper)
        {
            _townShipCodeRepository = townShipCodeRepository;
          
            _mapper = mapper;
        }

        public List<MerchantsResponseViewModel> GetAll()
        {
            //throw new ArgumentNullException("Dummy Exception Message");
            //throw new AppException("From Application Exception");
            var models = _townShipCodeRepository.GetAll();
            var viewModels = _mapper.Map<List<MerchantsResponseViewModel>>(models);
            return viewModels;
        }
       

        public async Task<List<MerchantsResponseViewModel>> GetAllAsync()
        {
            var models = await _townShipCodeRepository.GetAllAsync();
            var viewModels = _mapper.Map<List<MerchantsResponseViewModel>>(models);
            return viewModels;
        }

        public async Task<MerchantsResponseViewModel> GetAsync(Guid id)
        {
            var model = await _townShipCodeRepository.GetAsync(id);
            if (model == null)
                return null;
            var viewModel = _mapper.Map<MerchantsResponseViewModel>(model);
            return viewModel;
        }

        public async Task<bool> UpdateAsync(MerchantsRequestViewModel model)
        {
            var m = await _townShipCodeRepository.GetAsync(model.Id);
            if (m == null)
                return false;

            m.Name = model.Name;
            m.Email = model.Email;
            m.PhoneNo = model.PhoneNo;
            m.Address = model.Address;
            m.UpdatedDate = DateTime.UtcNow;

            await _townShipCodeRepository.UpdateAsync(m);
            return true;
        }

        public async Task<bool> AddAsync(MerchantsRequestViewModel viewModel)
        {
            var model = _mapper.Map<Merchant>(viewModel);
            model.CreatedDate = DateTime.UtcNow;
            model.UpdatedDate = DateTime.UtcNow;
            model.IsActive = true;
            model.IsDeleted = false;
            //Guid guid = new Guid();
           // model.CreatedStaffNo = "000";

            await _townShipCodeRepository.AddAsync(model);


        /*    User user = new User();
            user.Id = Guid.NewGuid();
            user.UserName = "Test00001";
            user.Email = "Test0001@gmail.com";
            user.PasswordHash = "111";
            user.RoleId = Guid.NewGuid();
            user.EmailConfirmed = true;
            user.PhoneNumberConfirmed = true;
            user.AccessFailedCount = 1;
            user.PhoneNumber = "";
            user.CreatedDate = DateTime.Now;
            user.CreatedStaffNo = model.CreatedStaffNo;

            await _usersRepository.AddAsync(user);*/
            //user.CreatedStaffNo = HttpContext.Session.GetString("StaffNo");

            return true;
        }

        public async Task<bool> DeleteAsync(Guid? id)
        {
            return await _townShipCodeRepository.DeleteAsync(id);
        }
    }
}
