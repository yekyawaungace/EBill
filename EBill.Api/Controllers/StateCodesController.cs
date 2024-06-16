using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelInsurance.Infrastructure.Dto.StateCode;
using TravelInsurance.Infrastructure.Dto.TownShipCode;
using TravelInsurance.Infrastructure.Dto.TransactionLogs;
using TravelInsurance.Infrastructure.Dto.User;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IServices;
using TravelInsurance.Repository.Ef;
using TravelInsurance.Service.Services;


namespace TravelInsuranceWebSite.Controllers
{
    public class StateCodesController : Controller
    {
        #region Variables
        private readonly ILogger<StateCodesController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IStateCodesService _townShipCodesService;
        private readonly ITransactionLogsService _transactionLogsService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public StateCodesController(IStateCodesService townShipCodesServiceService,
              ITransactionLogsService transactionLogsService,
            ILogger<StateCodesController> logger,
            ApplicationDbContext context,
              IMapper mapper)
        {
            ViewBag.SuccessMessage = "";
            ViewBag.ErrorMessage = "";
            _townShipCodesService = townShipCodesServiceService;
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
                // return RedirectToAction("Index", "Login");
                return false;
            }

            if (CheckExist(staffname) == 0)
            {

                //return RedirectToAction("Index", "Login");
                return false;
            }

            return true;
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

        public int CheckduplicateExist(string Name)
        {
            int count = 0;

            try
            {
                var query = (from user in _context.StateCodes
                             where user.StateCodeEN == Name && user.IsDeleted == false && user.IsActive == true
                             select user.StateCodeEN).ToList();
                count = query.Count;
            }
            catch { }

            return count;
        }
        #endregion

        #region Methods
        public async Task<IActionResult> Index()
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }
            return _context.StateCodes != null ?
                      View(await _context.StateCodes.OrderBy(x => x.StateCodeEN).ToListAsync()) :
                      Problem("Entity set 'ApplicationDbContext.StateCodes'  is null.");
        }

        public IActionResult Create()
        {
            CheckPermission();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(StateCodeRequest request)
        {
            CheckPermission();

            if (CheckduplicateExist(request.nrcEn) != 0)
            {
                ViewBag.ErrorMessage = "Duplicate Data";
                return View();
            }
            StateCode _statecode = new StateCode();
            _statecode.Id = Guid.NewGuid();
            _statecode.StateCodeEN = request.nrcEn;
            _statecode.StateCodeMM = request.nrcMm;
            _statecode.CreatedDate = DateTime.Now;
            _statecode.CreatedStaffNo = HttpContext.Session.GetString("StaffNo");


            await _townShipCodesService.AddAsync(_mapper.Map<StateCodesRequestViewModel>(_statecode));


            ViewBag.SuccessMessage = "Save Successful";

            return _context.StateCodes != null ?
                 View(await _context.StateCodes.OrderBy(x => x.StateCodeEN).ToListAsync()) :
                 Problem("Entity set 'ApplicationDbContext.StateCodes'  is null.");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            CheckPermission();
            if (id == null || _context.TownShipCodes == null)
            {
                return NotFound();
            }

            var townShipCodes = await _context.StateCodes.FindAsync(id);
            if (townShipCodes == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<StateCodesRequestViewModel>(townShipCodes));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,StateCodeEN,StateCodeMM,IsActive,IsDeleted,CreatedDate,CreatedUserID,UpdatedDate,UpdatedUserID")] StateCodesRequestViewModel townShipCodes)
        {
            CheckPermission();
            if (id != townShipCodes.Id)
            {
                return NotFound();
            }
            
            try
            {
                townShipCodes.IsActive = true;
                townShipCodes.IsDeleted = false;
                townShipCodes.CreatedDate = DateTime.Now;
                townShipCodes.CreatedStaffNo = HttpContext.Session.GetString("StaffNo");
                townShipCodes.UpdatedStaffNo = HttpContext.Session.GetString("StaffNo");
                townShipCodes.UpdatedDate = DateTime.Now;

                await _townShipCodesService.UpdateAsync(townShipCodes);
            
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateCodesExists(townShipCodes.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            // }
            // return View(townShipCodes);
        }

        private bool StateCodesExists(Guid id)
        {
            return (_context.StateCodes?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        #endregion

    }

}
