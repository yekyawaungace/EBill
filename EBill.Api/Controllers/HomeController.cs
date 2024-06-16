using Microsoft.AspNetCore.Mvc;
using TravelInsurance.Repository.Ef;


namespace TravelInsuranceWebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;

      /*  public HomeController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }
*/

        public HomeController(Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment,ILogger<HomeController> logger, ApplicationDbContext context)
        {
            Environment = _environment;
            _logger = logger;
            _context = context;
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

        public IActionResult Index()
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

        [HttpPost]
        public IActionResult Index(List<IFormFile> postedFiles)
        {
            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;

            string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in postedFiles)
            {
                string fileName = Path.GetFileName(postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                    ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
                }
            }

            return View();
        }


    }
}