using Microsoft.AspNetCore.Mvc;
using TravelInsurance.Repository.Ef;



namespace TravelInsuranceWebSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PremiumFeesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PremiumFeesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("deletePremiumFees")]
        public void DeletePremiumFees([FromBody] MyModel3 model)
        {
            var customerInDb = _context.PremiumFees.SingleOrDefault(c => c.Id == model.id);

            if (customerInDb != null)
            {
                _context.PremiumFees.Remove(customerInDb);
                _context.SaveChanges();
            }
        }

        [HttpPost("GetPremiumFees")]
        public IActionResult GetPremiumFees()
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
                var userData = (from tempuser in _context.PremiumFees select tempuser);
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    userData = userData.OrderBy(s => sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    userData = userData.Where(m => m.Duration.Equals(searchValue)
                    || m.Fee.Equals(searchValue)

                                               );
                }
                recordsTotal = userData.Count();
                //var data = userData.Skip(skip).Take(pageSize).ToList().OrderBy(x => x.Duration);
                var data = userData.OrderBy(x => x.Duration);

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
