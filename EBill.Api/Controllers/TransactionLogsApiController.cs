using Microsoft.AspNetCore.Mvc;
using TravelInsurance.Repository.Ef;



namespace TravelInsuranceWebSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionLogsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransactionLogsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("deleteTransactionLogs")]
        public void DeleteTransactionLogs([FromBody] MyModel3 model)
        {
            var customerInDb = _context.TransactionLogs.SingleOrDefault(c => c.Id == model.id);

            if (customerInDb != null)
            {
                _context.TransactionLogs.Remove(customerInDb);
                _context.SaveChanges();
            }
        }

        [HttpPost("GetTransactionLogs")]
        public IActionResult GetTransactionLogs()
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
                /* var userData = (
                 from tempuser in _context.TransactionLogs 
                   select tempuser
                   );*/

                var userData = from u in _context.TransactionLogs
                               select new
                            {
                                Id = u.Id,
                                merchantID = u.merchantID == null ? "" : u.merchantID,
                                invoiceNo = u.invoiceNo == null ? "" : u.invoiceNo,
                                cardNo = u.cardNo == null ? "" : u.cardNo,
                                amount = u.amount ,
                                currencyCode = u.currencyCode == null ? "" : u.currencyCode,
                                tranRef = u.tranRef == null ? "" : u.tranRef,
                                referenceNo = u.referenceNo == null ? "" : u.referenceNo,
                                approvalCode = u.approvalCode == null ? "" : u.approvalCode,
                                eci = u.eci == null ? "" : u.eci,
                                transactionDateTime = u.transactionDateTime,
                                CreatedDateTime = u.CreatedDateTime,
                                respCode = u.respCode == null ? "" : u.respCode,
                                respDesc = u.respDesc == null ? "" : u.respDesc,
                                Remark =u.Remark == null ? "" : u.Remark,
                                paymentID = u.paymentID == null ? "" : u.paymentID
    };



                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    userData = userData.OrderBy(s => sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    userData = userData.Where(m => m.invoiceNo.Contains(searchValue)

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
        }

        public class MyModel3
        {
            public Guid? id { get; set; }
        }
    }
}
