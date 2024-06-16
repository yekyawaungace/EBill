
using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelInsurance.Infrastructure.Dto.Merchants;
using TravelInsurance.Infrastructure.Dto.PremiumFees;
using TravelInsurance.Infrastructure.Dto.TransactionLogs;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IServices;
using TravelInsurance.Repository.Ef;
using TravelInsurance.Service.Services;


namespace TravelInsuranceWebSite.Controllers
{
    public class PremiumFeesController : Controller
    {

        #region Variables
        private readonly ILogger<PremiumFeesController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IPremiumFeesService _premiumFeesService;
        private readonly ITransactionLogsService _transactionLogsService;
        private readonly IMapper _mapper;
        #endregion


        #region Constructor
        public PremiumFeesController(IPremiumFeesService premiumFeesService,
           ITransactionLogsService transactionLogsService,
           ILogger<PremiumFeesController> logger, ApplicationDbContext context,
              IMapper mapper)
        {
            _premiumFeesService = premiumFeesService;
            _transactionLogsService = transactionLogsService;
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region Helper
        public IActionResult CheckPermission()
        {
            string staffname = HttpContext.Session.GetString("StaffNo");
            if (staffname == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (CheckExist(staffname) == 0)
            {

                return RedirectToAction("Index", "Login");
            }

            return View();
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

        public int CheckduplicateExist(int? duration, double? fee)
        {
            int count = 0;

            try
            {
                var query = (from user in _context.PremiumFees
                             where user.Duration == duration
                             //&& user.Fee == fee 
                             && user.IsDeleted == false && user.IsActive == true
                             select user.Duration).ToList();
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
            return CheckPermission();
        }

        public IActionResult Create()
        {
            CheckPermission();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(PremiumFeesRequestViewModel request)
        {
            CheckPermission();
            if (CheckduplicateExist(request.Duration, request.Fee) != 0)
            {
                ViewBag.ErrorMessage = "Duplicate Duration";
                return View();
            }
            request.IsActive = true;
            request.IsDeleted = false;
            request.CreatedDate = DateTime.Now;
            request.CreatedStaffNo = HttpContext.Session.GetString("StaffNo");
            await _premiumFeesService.AddAsync(request);

            ViewBag.SuccessMessage = "Save Successful";
            TempData["Message"] = "Save Successful";
            return RedirectToAction("Index");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            CheckPermission();
            if (id == null || _context.PremiumFees == null)
            {
                return NotFound();
            }

            var townShipCodes = await _context.PremiumFees.FindAsync(id);
            if (townShipCodes == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<PremiumFeesRequestViewModel>(townShipCodes));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Duration,Fee,IsActive,IsDeleted,CreatedDate,CreatedUserID,UpdatedDate,UpdatedUserID")] PremiumFeesRequestViewModel townShipCodes)
        {
            CheckPermission();
            if (id != townShipCodes.Id)
            {
                return NotFound();
            }

            try
            {
                townShipCodes.UpdatedDate = DateTime.Now;
                townShipCodes.IsActive = true;
                townShipCodes.IsDeleted = false;
                townShipCodes.CreatedDate = DateTime.Now;
                townShipCodes.CreatedStaffNo = HttpContext.Session.GetString("StaffNo");
                townShipCodes.CreatedDate = DateTime.Now;
                townShipCodes.UpdatedDate = DateTime.Now;

                await _premiumFeesService.UpdateAsync(townShipCodes);
              
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PremiumFeesExists(townShipCodes.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            TempData["Message"] = "Update Successful";
            return RedirectToAction("Index");

        }

        private bool PremiumFeesExists(Guid id)
        {
            return (_context.PremiumFees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        #endregion



    }

}
