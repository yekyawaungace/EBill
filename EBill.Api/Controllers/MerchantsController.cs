using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelInsurance.Infrastructure.Dto.Merchants;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IServices;
using TravelInsurance.Repository.Ef;
using QRCoder;
using AutoMapper;


namespace TravelInsuranceWebSite.Controllers
{
    public class MerchantsController : Controller
    {

        #region Variables
        private readonly ILogger<MerchantsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMerchantsService _townShipCodesService;
        private readonly IUsersService _usersService;
        private readonly ITransactionLogsService _transactionLogsService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        #endregion


        #region Constructor
        public MerchantsController(ILogger<MerchantsController> logger, ApplicationDbContext context
           , IMerchantsService townShipCodesService,
           IUsersService userService, ITransactionLogsService transactionLogsService,
           IMapper mapper, IConfiguration configuration)
        {

            _logger = logger;
            _context = context;
            _townShipCodesService = townShipCodesService;
            _usersService = userService;
            _transactionLogsService = transactionLogsService;
            _mapper = mapper;
            _configuration = configuration;
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
                var query = (from user in _context.Merchants
                             where user.Name == Name && user.IsDeleted == false && user.IsActive == true
                             select user.Name).ToList();
                count = query.Count;
            }
            catch { }

            return count;
        }

        public int CheckduplicateExistInEdit(string Name , Guid id)
        {
            int count = 0;

            try
            {
                var query = (from user in _context.Merchants
                             where user.Name == Name && user.Id != id && user.IsDeleted == false && user.IsActive == true
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
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(MerchantsRequestViewModel request)
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

            request.QRCode = request.Name;
            request.Email = "";
            request.IsActive = true;
            request.IsDeleted = false;
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
            if (id == null || _context.PremiumFees == null)
            {
                return NotFound();
            }

            var townShipCodes = await _context.Merchants.FindAsync(id);
            if (townShipCodes == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<MerchantsRequestViewModel>(townShipCodes));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Address,PhoneNo,QRCode,Email, IsActive,IsDeleted,CreatedDate,CreatedUserID,UpdatedDate,UpdatedUserID")] MerchantsRequestViewModel merchantsRequestViewModel)
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }
            if (CheckduplicateExistInEdit(merchantsRequestViewModel.Name,merchantsRequestViewModel.Id) != 0)
            {
                ViewBag.ErrorMessage = "Duplicate Data";
                return View();
            }

            if (id != merchantsRequestViewModel.Id)
            {
                return NotFound();
            }
            try
            {
                merchantsRequestViewModel.QRCode = merchantsRequestViewModel.Name;
                merchantsRequestViewModel.Email = "";
                merchantsRequestViewModel.IsActive = true;
                merchantsRequestViewModel.IsDeleted = false;

                merchantsRequestViewModel.CreatedStaffNo = HttpContext.Session.GetString("StaffNo");
                merchantsRequestViewModel.UpdatedStaffNo = HttpContext.Session.GetString("StaffNo");

                merchantsRequestViewModel.CreatedDate = DateTime.Now;
                merchantsRequestViewModel.UpdatedDate = DateTime.Now;

                await _townShipCodesService.UpdateAsync(merchantsRequestViewModel);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MerchantsExists(merchantsRequestViewModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            ViewBag.SuccessMessage = "Update Successful";
            TempData["Message"] = "Update Successful";
            return RedirectToAction("Index");
            // return View(_mapper.Map<MerchantsRequestViewModel>(merchantsRequestViewModel));
        }

        private bool MerchantsExists(Guid id)
        {
            return (_context.Merchants?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // GET: PremiumFees/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null || _context.Merchants == null)
            {
                return NotFound();
            }

            var premiumFee = await _context.Merchants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (premiumFee == null)
            {
                return NotFound();
            }
            
            ViewBag.QRLink = _configuration.GetValue<string>("QR:qrlink") + "?merchant="+ id;
            return View(premiumFee);
        }

        [HttpPost]
        public async Task<IActionResult> Details(string qrcode, Guid? id)
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrcode, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    bitMap.Save(ms, ImageFormat.Png);
                    ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }

            /* return View();*/
            if (id == null || _context.Merchants == null)
            {
                return NotFound();
            }

            var premiumFee = await _context.Merchants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (premiumFee == null)
            {
                return NotFound();
            }

            return View(premiumFee);
        }

        #endregion


    }

}
