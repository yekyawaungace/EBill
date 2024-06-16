using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelInsurance.Infrastructure.Dto.NRCTypes;
using TravelInsurance.Infrastructure.Dto.StateCode;
using TravelInsurance.Infrastructure.Dto.TownShipCode;
using TravelInsurance.Infrastructure.Dto.TransactionLogs;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IServices;
using TravelInsurance.Repository.Ef;


namespace TravelInsuranceWebSite.Controllers
{
    public class NRCTypesController : Controller
    {

        #region Variables
        private readonly ILogger<NRCTypesController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly INRCTypesService _townShipCodesService;
        private readonly ITransactionLogsService _transactionLogsService;
        #endregion

        #region Constructor
        public NRCTypesController(INRCTypesService townShipCodesServiceService,
             ITransactionLogsService transactionLogsService, ILogger<NRCTypesController> logger, ApplicationDbContext context)
        {
            _townShipCodesService = townShipCodesServiceService;
            _transactionLogsService = transactionLogsService;
            _logger = logger;
            _context = context;
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

        public int CheckduplicateExist(string Name)
        {
            int count = 0;

            try
            {
                var query = (from user in _context.NRCTypes
                             where user.TypeEN == Name && user.IsDeleted == false && user.IsActive == true
                             select user.TypeEN).ToList();
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
        public async Task<IActionResult> Index(NRCTypesRequestViewModel request)
        {
            CheckPermission();

            if (CheckduplicateExist(request.TypeEN) != 0)
            {
                ViewBag.ErrorMessage = "Duplicate NRCType";
                return View();
            }


            request.Id = Guid.NewGuid();
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
            CheckPermission();
            if (id == null || _context.NRCTypes == null)
            {
                return NotFound();
            }

            var townShipCodes = await _context.NRCTypes.FindAsync(id);
            if (townShipCodes == null)
            {
                return NotFound();
            }
            //var stateCode = _context.StateCodes.ToList();

            // Pass the list of products to the view
            //ViewBag.StateCodeList = new SelectList(stateCode, "StateCodeEN", "StateCodeMM");
            return View(townShipCodes);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,TypeEN,TypeMM,IsActive,IsDeleted,CreatedDate,CreatedUserID,UpdatedDate,UpdatedUserID")] NRCType townShipCodes)
        {
            CheckPermission();

            if (CheckduplicateExist(townShipCodes.TypeEN) != 0)
            {
                ViewBag.ErrorMessage = "Duplicate NRCType";
                return View();
            }

            if (id != townShipCodes.Id)
            {
                return NotFound();
            }

           
            try
            {
                townShipCodes.UpdatedDate = DateTime.Now;
                Guid guid = new Guid();

                _context.Update(townShipCodes);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NRCTypesExists(townShipCodes.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            
            //return View(townShipCodes);
        }

        private bool NRCTypesExists(Guid id)
        {
            return (_context.NRCTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        #endregion


    }

}
