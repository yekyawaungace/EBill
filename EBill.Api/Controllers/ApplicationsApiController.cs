using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Globalization;
using TravelInsurance.Repository.Ef;


namespace TravelInsuranceWebSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApplicationsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("deleteApplications")]
        public void DeleteApplications([FromBody] MyModel3 model)
        {
            var customerInDb = _context.Applications.SingleOrDefault(c => c.Id == model.id);

            if (customerInDb != null)
            {
                _context.Applications.Remove(customerInDb);
                _context.SaveChanges();
            }
        }

        [HttpGet("GetApplications")]
        public IActionResult GetApplications(string applicatonId,
            string merchantName,
            string name,
            DateTime receivedDate,
            DateTime startDate,
             DateTime endDate,
             string endosement,
            string paymentStatus
          
            )
        {
            try
            {
                var query = from u in _context.Applications
                            join t in _context.Travellers on u.Id equals t.OrderId
                            join m in _context.Merchants on u.Merchant equals m.Id.ToString() into merchantGroup
                            from merchant in merchantGroup.DefaultIfEmpty()
                            select new
                            {
                                Id = u.Id,
                                orderNo = u.OrderNo,
                                noofPeople = u.NoofPeople,
                                contactPhone = u.ContactPhone,
                                merchant = u.Merchant,
                                merchantname = merchant != null ? merchant.Name : "-",
                                receivedDate = u.ReceivedDate,
                                address = u.Address,
                                startDate = u.StartDate,
                                endDate = u.EndDate,
                                secondContactPerson = u.SecondContactPerson,
                                secondContactPhone = u.SecondContactPhone,
                                endosement = u.Endosement,
                                paymentStatus = u.PaymentStatus == 0 ? "Unpaid" : "Paid",
                                name = t.Name
                            };

                if (applicatonId != "-")
                {
                    query = query.Where(u => u.orderNo == applicatonId);
                }

                if (merchantName != "-")
                {
                    query = query.Where(u => u.merchantname == merchantName);
                }

                if (name != "-")
                {
                    query = query.Where(u => u.name == name || u.secondContactPerson == name);
                }

                if (receivedDate !=  DateTime.MinValue)
                {
                    query = query.Where(u => u.receivedDate.Date == receivedDate.Date);
                   
                }

                if (startDate != DateTime.MinValue)
                {
                    query = query.Where(u => u.startDate.Date == startDate.Date);

                }

                if (endDate != DateTime.MinValue)
                {
                    query = query.Where(u => u.endDate.Date == endDate.Date);

                }

                if (endosement !="-")
                {
                    query = query.Where(u => u.endosement == Convert.ToInt16( endosement));

                }
               

                if (paymentStatus != "-")
                {
                    int status = Convert.ToInt16(paymentStatus);
                    
                    query = query.Where(u => u.paymentStatus.Equals(status == 0 ? "Unpaid" : "Paid"));
                }

                var userData = query
                    .OrderBy(x => x.orderNo)
                    .AsEnumerable()
                    .Select((data, index) => new
                    {
                        srno = index + 1,
                        data.Id,
                        data.orderNo,
                        data.noofPeople,
                        data.contactPhone,
                        data.address,
                        data.startDate,
                        data.endDate,
                        data.secondContactPerson,
                        data.secondContactPhone,
                        data.paymentStatus,
                        data.merchantname
                    });

                var jsonData = new { data = userData };

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
