
using Microsoft.AspNetCore.Mvc;
using TravelInsurance.Infrastructure.Dto.User;
using TravelInsurance.Repository.Ef;



namespace TravelInsuranceWebSite.Controllers
{
    public class loginController : Controller
    {
        private readonly ILogger<loginController> _logger;
        private readonly ApplicationDbContext _context;
       // private readonly IMerchantsService _townShipCodesService;
        public loginController(ILogger<loginController> logger, ApplicationDbContext context)
        {
         
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            @ViewBag.username = "";
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string passcode)
        {

            var model = new StaffModel();
            List<StaffModel> lst = (from staff in _context.Users
                                    join permi in _context.Permission on staff.UserName equals permi.staffno
                                    join role in _context.Roles on permi.roleid equals role.Id
                                    where staff.Email == username && staff.PasswordHash == passcode
                                    select new StaffModel
                                    {
                                        StaffID = staff.Id,
                                        staffname = staff.UserName,
                                    }).ToList();

            if (lst.Count > 0)
            {
               
                HttpContext.Session.SetString("StaffID", Convert.ToString(lst[0].StaffID));
                HttpContext.Session.SetString("StaffNo", lst[0].staffname);
                ViewBag.username = HttpContext.Session.GetString("StaffNo");
                return RedirectToAction("Index", "Applications");

            }
            else
            {

                ViewBag.username = "Invalid Username or Password.";
                return View();
            }

            /* var issuccess = _loginUser.AuthenticateUser(username, passcode);
             if (issuccess.Result != null)
             {
                 ViewBag.username = string.Format("Successfully logged-in", username);

                 TempData["username"] = "Ahmed";
                 return RedirectToAction("Index", "Layout");
             }
             else
             {
                 ViewBag.username = string.Format("Login Failed ", username);
                 return View();
             }*/

            return View();
        }



        [HttpPost]
        public JsonResult CheckForLogIn(string username, string password)
        {
            var model = new StaffModel();
            List<StaffModel> lst = (from staff in _context.Users
                                    join permi in _context.Permission on staff.UserName equals permi.staffno
                                    join role in _context.Roles on permi.roleid equals role.Id
                                    where staff.Email == username && staff.PasswordHash == password
                                    select new StaffModel
                                    {
                                        StaffID = staff.Id,
                                        staffname = staff.UserName,
                                    }).ToList();

            if (lst.Count > 0)
            {
                model.MessageType = 1;
                HttpContext.Session.SetString("StaffID", Convert.ToString( lst[0].StaffID));
                HttpContext.Session.SetString("StaffNo", lst[0].staffname);
                ViewBag.StaffNo = HttpContext.Session.GetString("StaffNo");

            }
            else
            {
                 model.MessageType = 2;
                 model.Message = "Invalid Username or Password.";
            }


            return Json(model);
        }

    }

}
