using Microsoft.AspNetCore.Mvc;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Repository.Ef;



namespace TravelInsuranceWebSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TownShipCodesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TownShipCodesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("deleteTownShipCodes")]
        public void DeleteTownShipCodes([FromBody] MyModel3 model)
        {
            var customerInDb = _context.TownShipCodes.SingleOrDefault(c => c.Id == model.id);

            if (customerInDb != null)
            {
                _context.TownShipCodes.Remove(customerInDb);
                _context.SaveChanges();
            }
        }


        [HttpGet("GetTownShipCodes")]
        public IActionResult GetTownShipCodes(Guid departmentId)
        {
            try
            {
                var userData = (from t in _context.TownShipCodes
                                join s in _context.StateCodes on t.StateCodeId equals s.Id
                                where(t.StateCodeId == departmentId)
                                select new
                                {
                                    Id = t.Id,
                                    codeEN = t.CodeEN,
                                    codeMM = t.CodeMM,
                                    Statecode = s.StateCodeEN
                                }).OrderBy(x => x.codeEN)
                .AsEnumerable() // This is important to switch to LINQ to Objects
                .Select((data, index) => new
                {
                    srno = index + 1,
                    data.Id,
                    data.codeEN,
                    data.codeMM,
                    data.Statecode
                });

                //int count = userData.Count();

                var jsonData = new { data = userData };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /* [HttpPost("GetTownShipCodes")]
         public IActionResult GetTownShipCodes()
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

                 var userData = (from t in _context.TownShipCodes
                                 join s in _context.StateCodes on t.StateCodeId equals s.Id
                                 select new
                                 {
                                     Id = t.Id,
                                     codeEN = t.CodeEN,
                                     codeMM = t.CodeMM,
                                     Statecode = s.StateCodeEN
                                 }).OrderBy(x => x.codeEN)
                 .AsEnumerable() // This is important to switch to LINQ to Objects
                 .Select((data, index) => new
                 {
                     srno = index + 1,
                     data.Id,
                     data.codeEN,
                     data.codeMM,
                     data.Statecode
                 });

                 if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                 {
                     userData = userData.OrderBy(s => sortColumn + " " + sortColumnDirection);
                 }
                 if (!string.IsNullOrEmpty(searchValue))
                 {
                     userData = userData.Where(m => m.codeEN.Contains(searchValue)

                                                );
                 }
                 recordsTotal = userData.Count();
                 var data = userData.Skip(skip).Take(pageSize).ToList();
                 var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                 return Ok(jsonData);
             }
             catch (Exception ex)
             {
                 throw;
             }
         }*/

        public class MyModel3
        {
            public Guid? id { get; set; }
        }
    }
}
