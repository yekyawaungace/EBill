using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelInsurance.Infrastructure.Dto.Merchants;
using TravelInsurance.Infrastructure.Dto.TownShipCode;
using TravelInsurance.Infrastructure.Dto.TransactionLogs;
using TravelInsurance.Infrastructure.Dto.User;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IServices;
using TravelInsurance.Repository.Ef;
using TravelInsurance.Service.Services;


namespace TravelInsuranceWebSite.Controllers
{
    public class TownShipCodesController : Controller
    {
        #region Variables
        private readonly ILogger<TownShipCodesController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly ITownShipCodeService _townShipCodesService;
        private readonly ITransactionLogsService _transactionLogsService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public TownShipCodesController(ITownShipCodeService townShipCodesServiceService,
             ITransactionLogsService transactionLogsService, ILogger<TownShipCodesController> logger,
           ApplicationDbContext context,
           IMapper mapper)
        {
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
                var query = (from user in _context.TownShipCodes
                             where user.CodeEN == Name && user.IsDeleted == false && user.IsActive == true
                             select user.CodeEN).ToList();
                count = query.Count;
            }
            catch { }

            return count;
        }
        public int CheckduplicateExistInEdit(string Name,Guid id)
        {
            int count = 0;

            try
            {
                var query = (from user in _context.TownShipCodes
                             where user.CodeEN == Name && user.Id != id && user.IsDeleted == false && user.IsActive == true
                             select user.CodeEN).ToList();
                count = query.Count;
            }
            catch { }

            return count;
        }
        public bool CheckduplicateExistName(string Name)
        {
            int count = 0;

            try
            {
                var query = (from user in _context.TownShipCodes
                             where user.CodeEN == Name && user.IsDeleted == false && user.IsActive == true
                             select user.CodeEN).ToList();
                count = query.Count;
                if (count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch {  }
            return false;

        }

        public bool CheckduplicateExistNotName(string Name)
        {
            int count = 0;

            try
            {
                var query = (from user in _context.TownShipCodes
                             where user.CodeEN == Name && user.IsDeleted == false && user.IsActive == true
                             select user.CodeEN).ToList();
                count = query.Count;
                if (count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch { }
            return false;

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
            
            //ViewBag.StateCodesDropDownlist = _context.StateCodes.ToList().OrderBy(x => x.StateCodeEN);
            ViewBag.StateCodesDropDownlist = _context.StateCodes
 .ToList()
 .OrderBy(x => !string.IsNullOrEmpty(x.StateCodeEN) ? int.Parse(x.StateCodeEN) : 0);




            return View();
        }

        public IActionResult Create()
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }

           // var stateCode = _context.StateCodes.ToList().OrderBy(x => x.StateCodeEN);

            var stateCode = _context.StateCodes
    .ToList()
    .OrderBy(x => !string.IsNullOrEmpty(x.StateCodeEN) ? int.Parse(x.StateCodeEN) : 0);

            ViewBag.StateCodeList = new SelectList(stateCode, "Id", "StateCodeEN");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TownShipCodeRequestViewModel request)
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }
            var stateCode = _context.StateCodes.ToList().OrderBy(x => x.StateCodeEN);

            //Remove CheckduplicateExist for requirment
            /* if (CheckduplicateExist(request.CodeEN) != 0)
             {
                 ViewBag.StateCodeList = new SelectList(stateCode, "Id", "StateCodeEN");
                 ViewBag.ErrorMessage = "Duplicate Data";
                 return View();
             }*/

            request.Id = Guid.NewGuid();
            request.CreatedStaffNo = HttpContext.Session.GetString("StaffNo");
            await _townShipCodesService.AddAsync(request);

            var role = _context.Roles.ToList();

            // Pass the list of products to the view
            //var stateCode = _context.StateCodes.ToList().OrderBy(x => x.StateCodeEN);

            ViewBag.StateCodeList = new SelectList(stateCode, "Id", "StateCodeEN");
            ViewBag.SuccessMessage = "Save Successful";
            TempData["Message"] = "Save Successful";
            return RedirectToAction("Index");
            //return View(request);
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
            if (id == null || _context.TownShipCodes == null)
            {
                return NotFound();
            }

            var townShipCodes = await _context.TownShipCodes.FindAsync(id);
            if (townShipCodes == null)
            {
                return NotFound();
            }
            //var stateCode = _context.StateCodes.ToList().OrderBy(x => x.StateCodeEN);
            var stateCode = _context.StateCodes
  .ToList()
  .OrderBy(x => !string.IsNullOrEmpty(x.StateCodeEN) ? int.Parse(x.StateCodeEN) : 0);

            // Pass the list of products to the view
            ViewBag.StateCodeList = new SelectList(stateCode, "Id", "StateCodeEN");
            ViewBag.SelectedStateCode = townShipCodes.StateCodeId;
            return View(_mapper.Map<TownShipCodeRequestViewModel>(townShipCodes));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,StateCodeId,CodeEN,CodeMM,IsActive,IsDeleted,CreatedDate,CreatedUserID,UpdatedDate,UpdatedUserID")] TownShipCodeRequestViewModel townShipCodes)
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }
            var stateCode = _context.StateCodes.ToList().OrderBy(x => x.StateCodeEN);
            //Remove CheckduplicateExistInEdit for requirment
            /*  if (CheckduplicateExistInEdit(townShipCodes.CodeEN, townShipCodes.Id) != 0)
            {
                ViewBag.StateCodeList = new SelectList(stateCode, "Id", "StateCodeEN");
                ViewBag.ErrorMessage = "Duplicate Data";
                return View();
            }*/
            if (id != townShipCodes.Id)
            {
                return NotFound();
            }

            try
            {
                townShipCodes.UpdatedDate = DateTime.Now;
                townShipCodes.CreatedStaffNo = HttpContext.Session.GetString("StaffNo");
                await _townShipCodesService.UpdateAsync(townShipCodes);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TownShipCodesExists(townShipCodes.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            ViewBag.SuccessMessage = "Update Successful";
           // var stateCode = _context.StateCodes.ToList().OrderBy(x => x.StateCodeEN);

            ViewBag.StateCodeList = new SelectList(stateCode, "Id", "StateCodeEN");
            TempData["Message"] = "Update Successful";
            return RedirectToAction("Index");
         
        }
        private bool TownShipCodesExists(Guid id)
        {
            return (_context.TownShipCodes?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        #endregion

    }

}
