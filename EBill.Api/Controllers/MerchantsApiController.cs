using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TravelInsurance.Infrastructure.IServices;
using TravelInsurance.Repository.Ef;



namespace TravelInsuranceWebSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MerchantsController> _logger;
        private readonly IMerchantsService _townShipCodesService;

        public MerchantsApiController(ILogger<MerchantsController> logger, ApplicationDbContext context
            , IMerchantsService townShipCodesService)
        {
            _logger = logger;
            _context = context;
            _townShipCodesService = townShipCodesService;
        }

        [HttpPost("deleteMerchants")]
        public void DeleteMerchants([FromBody] MyModel3 model)
        {
            var customerInDb = _context.Merchants.SingleOrDefault(c => c.Id == model.id);

            if (customerInDb != null)
            {
                //    _townShipCodesService.DeleteAsync(model.id);
                _context.Entry(customerInDb).State = EntityState.Deleted;
                _context.SaveChanges();


                //_context.Merchants.Remove(customerInDb);
                //_context.SaveChangesAsync();
            }
        }

        [HttpPost("GetMerchants")]
        public IActionResult GetMerchants()
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
               // var userData = (from tempuser in _context.Merchants select tempuser);

                var userData = (from u in _context.Merchants
                                select new
                                {
                                    Id = u.Id,
                                    name = u.Name,
                                    phoneNo = u.PhoneNo,
                                    address = u.Address

                                }

                                ).OrderBy(x => x.name)
              .AsEnumerable() // This is important to switch to LINQ to Objects
              .Select((data, index) => new
              {
                  srno = index + 1,
                  data.Id,
                  data.name,
                  data.phoneNo,
                  data.address
              });

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    userData = userData.OrderBy(s => sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    userData = userData.Where(m => m.name.Contains(searchValue) ||
                    m.phoneNo.Contains(searchValue) || m.address.Contains(searchValue)
                                               );
                }
                recordsTotal = userData.Count();
                var data = userData.Skip(skip).Take(pageSize).ToList().OrderBy(x => x.name);
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
