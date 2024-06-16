using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelInsurance.Infrastructure.Dto.Permission;
using TravelInsurance.Infrastructure.Dto.User;
using TravelInsurance.Infrastructure.IServices;
using TravelInsurance.Repository.Ef;



namespace TravelInsuranceWebSite.Controllers
{
    public class UsersController : Controller
    {
        #region Variables
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersService _usersService;
        private readonly IPermissionService _permissionService;
        private readonly ITransactionLogsService _transactionLogsService;
        private readonly IMapper _mapper;

        #endregion


        #region Constructor
        public UsersController(IUsersService usersService,
           IPermissionService permissionService,
           ITransactionLogsService transactionLogsService,
           ILogger<UsersController> logger, ApplicationDbContext context,
           IMapper mapper)
        {
            _usersService = usersService;
            _permissionService = permissionService;
            _transactionLogsService = transactionLogsService;
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }
        #endregion


        #region Helper
        public bool CheckPermission()
        {
            string staffname = HttpContext.Session.GetString("StaffNo");
            if (staffname == null)
            {
                return false;
                //return RedirectToAction("Index", "Login");
            }

            if (CheckExist(staffname) == 0)
            {
                return false;
                //return RedirectToAction("Index", "Login");
            }
            return true;
            //return View();
        }

        public int CheckExist(string StaffNo)
        {
            int count = 0;

            try
            {
                var query = (from per in _context.Permission
                             join role in _context.Roles on per.roleid equals role.Id
                             where per.staffno == StaffNo && per.isdeleted == false && role.IsDeleted == false
                             select per.staffno).ToList();
                count = query.Count;
            }
            catch { }

            return count;
        }

        public int CheckduplicateExist(string StaffNo)
        {
            int count = 0;

            try
            {
                var query = (from user in _context.Users
                             where user.Email == StaffNo && user.IsDeleted == false && user.IsActive == true
                             select user.UserName).ToList();
                count = query.Count;
            }
            catch { }

            return count;
        }

        public int CheckduplicateExistInEdit(string StaffNo,Guid id)
        {
            int count = 0;

            try
            {
                var query = (from user in _context.Users
                             where user.UserName == StaffNo && user.Id != id && user.IsDeleted == false && user.IsActive == true
                             select user.UserName).ToList();
                count = query.Count;
            }
            catch { }

            return count;
        }


        #endregion


        #region Methods
        public IActionResult Index()
        {
            ViewBag.SuccessMessage = TempData["Message"];
            
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Create()
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }
            var role = _context.Roles.ToList();

            ViewBag.RoleList = new SelectList(role, "Id", "RoleName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,RoleId,Email,EmailConfirmed,PasswordHash,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,AccessFailedCount,IsActive,IsDeleted,CreatedDate,CreatedStaffNo,UpdatedDate,UpdatedStaffNo")] UserRequestViewModel user)
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }
            if (CheckduplicateExist(user.Email) != 0)
            {
                var duplicaterole = _context.Roles.ToList();

                // Pass the list of products to the view
                ViewBag.RoleList = new SelectList(duplicaterole, "Id", "RoleName");
                ViewBag.ErrorMessage = "Duplicate Data";
                return View();
            }

            user.Id = Guid.NewGuid();
            user.EmailConfirmed = true;
            user.PhoneNumberConfirmed = true;
            user.AccessFailedCount = 1;
            user.PhoneNumber = "";
            user.CreatedDate = DateTime.Now;
            user.CreatedStaffNo = HttpContext.Session.GetString("StaffNo");

            await _usersService.AddAsync(user);


            var pRequestViewModel = new PermissionRequestViewModel();
            pRequestViewModel.Id = Guid.NewGuid();
            pRequestViewModel.staffno = user.UserName;
            pRequestViewModel.CreatedStaffNo = HttpContext.Session.GetString("StaffNo");
            pRequestViewModel.roleid = user.RoleId;
            pRequestViewModel.isdeleted = false;
            pRequestViewModel.CreatedDate = DateTime.Now;

            await _permissionService.AddAsync(pRequestViewModel);

            var role = _context.Roles.ToList();

            // Pass the list of products to the view
            ViewBag.RoleList = new SelectList(role, "Id", "RoleName");
            ViewBag.SelectedRole = user.RoleId;
            ViewBag.SuccessMessage = "Save Successful";
            TempData["Message"] = "Save Successful";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Profile(string? name)
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }
            if (name == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
              .FirstOrDefaultAsync(m => m.UserName == name);
            if (user == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var role = _context.Roles.ToList();

            // Pass the list of products to the view
            ViewBag.RoleList = new SelectList(role, "Id", "RoleName");
            ViewBag.SelectedRole = user.RoleId;
            return View(_mapper.Map<UserRequestViewModel>(user));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserName,RoleId,Email,EmailConfirmed,PasswordHash,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,AccessFailedCount,IsActive,IsDeleted,CreatedDate,CreatedStaffNo,UpdatedDate,UpdatedStaffNo")] UserRequestViewModel user)
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }

            if (CheckduplicateExistInEdit(user.UserName,user.Id) != 0)
            {
                var duplicaterole = _context.Roles.ToList();

                // Pass the list of products to the view
                ViewBag.RoleList = new SelectList(duplicaterole, "Id", "RoleName");
                ViewBag.ErrorMessage = "Duplicate Data";
                return View();
            }
            if (id != user.Id)
            {
                return NotFound();
            }

            try
            {
                user.IsActive = true;
                user.IsDeleted = false;
                user.CreatedDate = DateTime.Now;
                user.CreatedStaffNo = HttpContext.Session.GetString("StaffNo");
                user.CreatedDate = DateTime.Now;
                user.UpdatedDate = DateTime.Now;

                await _usersService.UpdateAsync(user);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
           
            ViewBag.SuccessMessage = "Update Successful";
            var role = _context.Roles.ToList();

            // Pass the list of products to the view
            ViewBag.RoleList = new SelectList(role, "Id", "RoleName");
            ViewBag.SelectedRole = user.RoleId;

            TempData["Message"] = "Update Successful";
            return RedirectToAction("Index");
            //return View(_mapper.Map<UserRequestViewModel>(user));
        }

        public async Task<IActionResult> changePassword(Guid? id)
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var role = _context.Roles.ToList();

            // Pass the list of products to the view
            ViewBag.RoleList = new SelectList(role, "Id", "RoleName");
            ViewBag.SelectedRole = user.RoleId;
            return View(_mapper.Map<UserRequestViewModel>(user));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> changePassword(Guid id, [Bind("Id,UserName,RoleId,Email,EmailConfirmed,PasswordHash,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,AccessFailedCount,IsActive,IsDeleted,CreatedDate,CreatedStaffNo,UpdatedDate,UpdatedStaffNo")] UserRequestViewModel user)
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id != user.Id)
            {
                return NotFound();
            }

            try
            {
                user.IsActive = true;
                user.IsDeleted = false;
                user.CreatedDate = DateTime.Now;
                user.CreatedStaffNo = HttpContext.Session.GetString("StaffNo");
                user.CreatedDate = DateTime.Now;
                user.UpdatedDate = DateTime.Now;

                await _usersService.UpdatePasswordAsync(user);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            //return RedirectToAction(nameof(Index));
            // }
            ViewBag.SuccessMessage = "Update Successful";
            var role = _context.Roles.ToList();

            // Pass the list of products to the view
            ViewBag.RoleList = new SelectList(role, "Id", "RoleName");
            ViewBag.SelectedRole = user.RoleId;
            //return View(_mapper.Map<UserRequestViewModel>(user));
            TempData["Message"] = "Update Successful";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(Guid id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        #endregion


    }
}
