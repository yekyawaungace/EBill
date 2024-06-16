using AutoMapper;
using TravelInsurance.Infrastructure.Dto.User;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IRepositories;
using TravelInsurance.Infrastructure.IServices;

namespace TravelInsurance.Service.Services
{
    public class UsersService : IUsersService
    {
        #region Private
        private readonly IMapper _mapper;
        private readonly IUsersRepository _townShipCodeRepository;
        #endregion
        public UsersService(IUsersRepository townShipCodeRepository,
            IMapper mapper)
        {
            _townShipCodeRepository = townShipCodeRepository;
            _mapper = mapper;
        }

        public List<UserResponseViewModel> GetAll()
        {
            //throw new ArgumentNullException("Dummy Exception Message");
            //throw new AppException("From Application Exception");
            var models = _townShipCodeRepository.GetAll();
            var viewModels = _mapper.Map<List<UserResponseViewModel>>(models);
            return viewModels;
        }
       

        public async Task<List<UserResponseViewModel>> GetAllAsync()
        {
            var models = await _townShipCodeRepository.GetAllAsync();
            var viewModels = _mapper.Map<List<UserResponseViewModel>>(models);
            return viewModels;
        }

        public async Task<UserResponseViewModel> GetAsync(Guid id)
        {
            var model = await _townShipCodeRepository.GetAsync(id);
            if (model == null)
                return null;
            var viewModel = _mapper.Map<UserResponseViewModel>(model);
            return viewModel;
        }

        public async Task<bool> UpdateAsync(UserRequestViewModel model)
        {
            var m = await _townShipCodeRepository.GetAsync(model.Id);
            if (m == null)
                return false;


            m.UserName  = model.UserName;
            m.RoleId = model.RoleId;
            m.Email = model.Email;
            m.UpdatedDate = DateTime.UtcNow;

            await _townShipCodeRepository.UpdateAsync(m);
            return true;
        }

        public async Task<bool> UpdatePasswordAsync(UserRequestViewModel model)
        {
            var m = await _townShipCodeRepository.GetAsync(model.Id);
            if (m == null)
                return false;


            //m.UserName = model.UserName;
            //m.RoleId = model.RoleId;
            //m.Email = model.Email;
            m.PasswordHash = model.PasswordHash;
            m.UpdatedDate = DateTime.UtcNow;

            await _townShipCodeRepository.UpdateAsync(m);
            return true;
        }

        public async Task<bool> AddAsync(UserRequestViewModel viewModel)
        {
            var model = _mapper.Map<User>(viewModel);
            model.CreatedDate = DateTime.UtcNow;
            model.UpdatedDate = DateTime.UtcNow;
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

        public List<StaffModel> CheckForLogIn(string staff_no, string pwd)
        {
            return  _townShipCodeRepository.CheckForLogIn(staff_no,pwd);
        }
    }
}
