using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelInsurance.Infrastructure.Dto.Country;
using TravelInsurance.Infrastructure.Dto.TransactionLogs;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IServices;
using TravelInsurance.Repository.Ef;


namespace TravelInsuranceWebSite.Controllers
{
    public class CountryController : Controller
    {
        private readonly ILogger<CountryController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly ICountryService _townShipCodesService;
        private readonly ITransactionLogsService _transactionLogsService;
        public CountryController(ILogger<CountryController> logger,
             ITransactionLogsService transactionLogsService,
            ApplicationDbContext context, ICountryService townShipCodesService)
        {

            _logger = logger;
            _transactionLogsService = transactionLogsService;
            _context = context;
            _townShipCodesService = townShipCodesService;
        }

        #region Helper
        public bool CheckPermission()
        {
            string staffname = HttpContext.Session.GetString("StaffNo");
            if (staffname == null)
            {
                return false;
               
            }

            if (CheckExist(staffname) == 0)
            {
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
                var query = (from user in _context.Country
                             where user.Name == Name && user.IsDeleted == false && user.IsActive == true
                             select user.Name).ToList();
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
        public IActionResult Create()
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }
            var stateCode = _context.Country.ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CountryRequestViewModel request)
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }

            if (CheckduplicateExist(request.Name) != 0)
            {
                ViewBag.ErrorMessage = "Duplicate Name";
                return View();
            }

            request.Id = Guid.NewGuid();
            request.IsDeleted = false;
            request.IsActive = true;
            request.CreatedDate = DateTime.Now;
            request.CreatedStaffNo = HttpContext.Session.GetString("StaffNo");
            await _townShipCodesService.AddAsync(request);

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
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.Country == null)
            {
                return NotFound();
            }

            var townShipCodes = await _context.Country.FindAsync(id);
            if (townShipCodes == null)
            {
                return NotFound();
            }

            return View(townShipCodes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,IsActive,IsDeleted,CreatedDate,CreatedUserID,UpdatedDate,UpdatedUserID")] Country townShipCodes)
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }

            if (CheckduplicateExist(townShipCodes.Name) != 0)
            {
                ViewBag.ErrorMessage = "Duplicate Name";
                return View();
            }

            if (id != townShipCodes.Id)
            {
                return NotFound();
            }

            try
            {
                townShipCodes.UpdatedDate = DateTime.Now;
                _context.Update(townShipCodes);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(townShipCodes.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            // return View(townShipCodes);
        }
        private bool CountryExists(Guid id)
        {
            return (_context.Country?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        #endregion



    }

}
