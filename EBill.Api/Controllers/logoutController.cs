
using Microsoft.AspNetCore.Mvc;
using TravelInsurance.Infrastructure.Dto.User;
using TravelInsurance.Repository.Ef;



namespace TravelInsuranceWebSite.Controllers
{
    public class logoutController : Controller
    {
        private readonly ILogger<logoutController> _logger;
        private readonly ApplicationDbContext _context;
     
        public logoutController(ILogger<logoutController> logger, ApplicationDbContext context)
        {
         
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            HttpContext.Session.Remove("StaffNo");
            return RedirectToAction("index", "login");
        }

    


    }

}
