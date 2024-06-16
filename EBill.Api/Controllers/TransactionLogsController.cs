using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelInsurance.Infrastructure.Dto.TownShipCode;
using TravelInsurance.Infrastructure.Dto.TransactionLogs;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IServices;
using TravelInsurance.Repository.Ef;


namespace TravelInsuranceWebSite.Controllers
{
    public class TransactionLogsController : Controller
    {
        private readonly ILogger<TransactionLogsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly ITransactionLogsService _townShipCodesService;
        public TransactionLogsController(ITransactionLogsService townShipCodesServiceService, ILogger<TransactionLogsController> logger, ApplicationDbContext context)
        {
            _townShipCodesService = townShipCodesServiceService;
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            /* string staffname = HttpContext.Session.GetString("StaffNo");
             if (staffname == null)
             {
                 return RedirectToAction("Index", "Login");
             }

             if (CheckExist(staffname) == 0)
             {

                 return RedirectToAction("Index", "Login");
             }
             return View();*/
            return CheckPermission();
        }

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

        public int CheckduplicateExist(string StaffNo)
        {
            int count = 0;

            try
            {
                var query = (from user in _context.Users
                             where user.UserName == StaffNo && user.IsDeleted == false && user.IsActive == true
                             select user.UserName).ToList();
                count = query.Count;
            }
            catch { }

            return count;
        }
        #endregion
        /* public int CheckExist(string StaffNo)
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
         }*/

        // GET: StateCodes/Create
        public IActionResult Create()
        {
            // var stateCode = new List<StateCode>

            var stateCode = _context.TransactionLogs.ToList();

            // Pass the list of products to the view
            ViewBag.StateCodeList = new SelectList(stateCode, "StateCodeEN", "StateCodeMM");

            return View();
        }

        // POST: StateCodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(TransactionLogsRequestViewModel request)
        {
            _townShipCodesService.AddAsync(request);
            return RedirectToAction("Index");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // GET: StateCodes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.TransactionLogs == null)
            {
                return NotFound();
            }

            var townShipCodes = await _context.TransactionLogs.FindAsync(id);
            if (townShipCodes == null)
            {
                return NotFound();
            }
            var stateCode = _context.StateCodes.ToList();

            // Pass the list of products to the view
            ViewBag.StateCodeList = new SelectList(stateCode, "StateCodeEN", "StateCodeMM");
            return View(townShipCodes);
        }

        // POST: StateCodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,StateCodeId,CodeEN,CodeMM,IsActive,IsDeleted,CreatedDate,CreatedUserID,UpdatedDate,UpdatedUserID")] TransactionLog townShipCodes)
        {
            if (id != townShipCodes.Id)
            {
                return NotFound();
            }

            /*  if (ModelState.IsValid)
              {*/
            try
            {
                //townShipCodes.UpdatedDate = DateTime.Now;
               // Guid guid = new Guid();
               // townShipCodes.StateCodeId = guid;

                _context.Update(townShipCodes);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionLogsExists(townShipCodes.Id))
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
            return View(townShipCodes);
        }

        private bool TransactionLogsExists(Guid id)
        {
            return (_context.TransactionLogs?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }

}
