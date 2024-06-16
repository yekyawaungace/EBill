using Microsoft.AspNetCore.Mvc;
using TravelInsurance.Repository.Ef;


namespace TravelInsuranceWebSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("deleteUsers")]
        public void DeleteUsers([FromBody] MyModel3 model)
        {
            var customerInDb = _context.Users.SingleOrDefault(c => c.Id == model.id);

            if (customerInDb != null)
            {
                _context.Users.Remove(customerInDb);
                _context.SaveChanges();
            }
        }

        [HttpPost("GetUsers")]
        public IActionResult GetUsers()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                /* var userData = (from u in _context.Users
                                 join e in _context.Roles on u.RoleId equals e.Id
                                 select new
                                 {
                                     Id = u.Id,
                                     userName = u.UserName,
                                     email = u.Email,
                                     role = e.RoleName
                                 }

                                 );

 */
                var userData = (from u in _context.Users
                                join e in _context.Roles on u.RoleId equals e.Id
                                select new
                                {
                                    Id = u.Id,
                                    userName = u.UserName,
                                    email = u.Email,
                                    role = e.RoleName
                                }

                                 ).OrderBy(x => x.userName)
               .AsEnumerable() // This is important to switch to LINQ to Objects
               .Select((data, index) => new
               {
                   srno = index + 1,
                   data.Id,
                   data.userName,
                   data.email,
                   data.role
               });

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    userData = userData.OrderBy(s => sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    userData = userData.Where(m => m.userName.Contains(searchValue)

                                               );
                }
                recordsTotal = userData.Count();
                var data = userData.Skip(skip).Take(pageSize).ToList().OrderBy(x => x.userName);
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public class MyModel3
        {
            public Guid? id { get; set; }
        }
    }
}
