using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelInsurance.Infrastructure.Dto.Applications;
using TravelInsurance.Infrastructure.Dto.Audit;
using TravelInsurance.Infrastructure.Dto.User;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Repository.Ef;
using OfficeOpenXml;
using System.IO;

namespace TravelInsuranceWebSite.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Helper
        public bool CheckPermission()
        {
            string staffname = HttpContext.Session.GetString("StaffNo");
            if (staffname == null)
            {
                return false;
                //return RedirectToAction("Index", "Login");
            }

            if (CheckExist(staffname) == 0)
            {
                return false;
                //return RedirectToAction("Index", "Login");
            }
            return true;
            //return View();
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

        public int CheckduplicateExist(string StaffNo)
        {
            int count = 0;

            try
            {
                var query = (from user in _context.Users
                             where user.UserName == StaffNo && user.IsDeleted == false && user.IsActive == true
                             select user.UserName).ToList();
                count = query.Count;
            }
            catch { }

            return count;
        }
        #endregion


        // GET: Applications
        public async Task<IActionResult> Index()
        {
            if(CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }

            var application = await (from u in _context.Applications
                                     join m in _context.Merchants on u.Merchant equals m.Id.ToString() into merchantGroup
                                     from merchant in merchantGroup.DefaultIfEmpty()
                                     select new
                            {
                                Id = u.Id,
                                orderNo = u.OrderNo,
                                noofPeople = u.NoofPeople,
                                contactPhone = u.ContactPhone,
                                type = u.Type == null ? "0" : u.Type,
                                         /*  merchant = u.Merchant == null ? "-" : u.Merchant,*/
                                         merchantname = merchant != null ? merchant.Name : "-",
                                         receivedDate = u.ReceivedDate == null ? DateTime.Now : u.ReceivedDate,
                                address = u.Address,
                                startDate = u.StartDate,
                                endDate = u.EndDate,
                                secondContactPerson = u.SecondContactPerson,
                                secondContactPhone = u.SecondContactPhone,
                                paymentStatus = u.PaymentStatus == 0 ? "Draft" : "Done",
                                endosement = u.Endosement == null ? "Draft" : ( u.Endosement == 0 ? "Draft" : "Done"),
                                certificateID = u.CertificateID == null ? "-" : u.CertificateID,
                                remark = u.Remark == null ? "-" : u.Remark

                            }

                                ).OrderBy(x => x.orderNo).ToListAsync();

            return _context.Applications != null ?
                        View(application) :
                        Problem("Entity set 'ApplicationDbContext.Applications'  is null.");
        }

        // Handle the filter form submission
        [HttpPost]
        public async Task<IActionResult> FilterApplications(string applicationId, string name, string merchantName, DateTime? receivedDate, DateTime? startDate, DateTime? endDate, string status, string endo, bool? paid, bool? unpaid)
        {
            // Implement your filtering logic here, using the provided filter criteria.
            var filteredApplications = _context.Applications.AsQueryable();

            if (!string.IsNullOrEmpty(applicationId))
            {
                //filteredApplications = filteredApplications.Where(app => app..Contains(applicationId));
            }

            if (!string.IsNullOrEmpty(name))
            {
                //filteredApplications = filteredApplications.Where(app => app.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(merchantName))
            {
                filteredApplications = filteredApplications.Where(app => app.Merchant.Contains(merchantName));
            }

            if (receivedDate != null)
            {
                filteredApplications = filteredApplications.Where(app => app.ReceivedDate == receivedDate);
            }

            if (startDate != null)
            {
                filteredApplications = filteredApplications.Where(app => app.StartDate == startDate);
            }

            if (endDate != null)
            {
                filteredApplications = filteredApplications.Where(app => app.EndDate == endDate);
            }

            if (!string.IsNullOrEmpty(status))
            {
                //filteredApplications = filteredApplications.Where(app => app.PaymentStatus == status);
            }

            if (!string.IsNullOrEmpty(endo))
            {
                //filteredApplications = filteredApplications.Where(app => app.en == endo);
            }

            if (paid != null)
            {
                //filteredApplications = filteredApplications.Where(app => app.PaymentStatus == "Paid");
            }

            if (unpaid != null)
            {
                //filteredApplications = filteredApplications.Where(app => app.PaymentStatus == "Unpaid");
            }

            var filteredData = await filteredApplications.ToListAsync();

            // Return the filtered data to the view
            return View("FilteredApplications", filteredData);
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }
        public async Task<IActionResult> ApplicationViewDetail(Guid? id)
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }

            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }
          /*  var application = await _context.Applications
               .FirstOrDefaultAsync(m => m.Id == id);*/

            var application = await (from u in _context.Applications
                                     join m in _context.Merchants on u.Merchant equals m.Id.ToString() into merchantGroup
                                     from merchant in merchantGroup.DefaultIfEmpty()
                                     select new ApplicationsDto
                                     {
                                         /*Id = u.Id,
                                         OrderNo = u.OrderNo,
                                         NoofPeople = u.NoofPeople,
                                         ContactPhone = u.ContactPhone,
                                         Type = u.Type == null ? "0" : u.Type,
                                         Description = u.Description,
                                         ReceivedDate = u.ReceivedDate,
                                         Merchant = u.Merchant == null ? "-" : u.Merchant,
                                         OverSeaOrDomestic = u.OverSeaOrDomestic,
                                         FromDestination = u.FromDestination,
                                         ToDestination = u.ToDestination,
                                         PremiumFee = u.PremiumFee,
                                         TransitionID = u.TransitionID,
                                         Address = u.Address,
                                         StartDate = u.StartDate,
                                         EndDate = u.EndDate,
                                         SecondContactPerson = u.SecondContactPerson,
                                         SecondContactPhone = u.SecondContactPhone,
                                         PaymentStatus = u.PaymentStatus,
                                         Endosement = u.Endosement,
                                         CertificateID = u.CertificateID == null ? "-" : u.CertificateID,
                                         Remark = u.Remark == null ? "-" : u.Remark,
                                         IsActive = u.IsActive,
                                         IsDeleted = u.IsDeleted,
                                         CreatedDate = u.CreatedDate,
                                         CreatedStaffNo = u.CreatedStaffNo,
                                         UpdatedDate = u.UpdatedDate,
                                         UpdatedStaffNo = u.UpdatedStaffNo*/
                                           Id = u.Id,
        OrderNo = u.OrderNo,
        NoofPeople = u.NoofPeople,
        Type = u.Type,
        Description = u.Description,
        ReceivedDate = u.ReceivedDate,
        Merchant = u.Merchant,
        MerchantName = merchant != null ? merchant.Name : "-",
        ContactPhone = u.ContactPhone,
        OverSeaOrDomestic  = u.OverSeaOrDomestic,
        FromDestination = u.FromDestination,
        ToDestination  = u.ToDestination,
        PremiumFee  = u.PremiumFee,
        TransitionID = u.TransitionID,
        Address = u.Address,
        StartDate  = u.StartDate,
        EndDate = u.EndDate,
        SecondContactPerson = u.SecondContactPerson,
        SecondContactPhone = u.SecondContactPhone,
        PaymentStatus = u.PaymentStatus,
        Endosement = u.Endosement == null ? 0 : u.Endosement,
        CertificateID  = u.CertificateID == null ? "-" : u.CertificateID,
        Remark = u.Remark == null ? "" : u.Remark,
        IsActive  = u.IsActive,
        IsDeleted =u.IsDeleted,
        CreatedDate = u.CreatedDate,
        CreatedStaffNo = u.CreatedStaffNo,
        UpdatedDate = u.UpdatedDate,
        UpdatedStaffNo = u.UpdatedStaffNo

    }
                               )
                                 .FirstOrDefaultAsync(m => m.Id == id);


            if (application == null)
            {
                return NotFound();
            }
            ViewBag.ApplicationId = application.Id;
            var auditTrailData = await _context.AuditTrails.Where(a => a.ApplicationId == id).ToListAsync();
            var travellerData = await _context.Travellers.Where(t => t.OrderId == id).ToListAsync();

            var viewModel = new ApplicationDetailView
            {
                Application = application,
                AuditTrail = auditTrailData,
                Traveller = travellerData
            };

            return View(viewModel);
        }


        // GET: Applications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderNo,NoofPeople,ContactPhone,OverSeaOrDomestic,FromDestination,ToDestination,PremiumFee,TransitionID,Address,StartDate,EndDate,SecondContactPerson,SecondContactPhone,PaymentStatus,IsActive,IsDeleted,CreatedDate,CreatedStaffNo,UpdatedDate,UpdatedStaffNo")] Application application)
        {
            if (ModelState.IsValid)
            {
                application.Id = Guid.NewGuid();
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(application);
        }

        [HttpPost]
        public async Task<IActionResult> AuditCreate(AuditRequestModel model)
        {
          
            try
            {
                AuditTrail audit = new AuditTrail();
                audit.Id = Guid.NewGuid();
                audit.Status = model.Status.ToString();
                audit.Remark = model.Remark == null ? "" : model.Remark;
                audit.CreatedStaffNo = HttpContext.Session.GetString("StaffNo") == null ? "-" : HttpContext.Session.GetString("StaffNo");
                Guid applicationGuid = new Guid(model.ApplicationId);
                audit.ApplicationId = applicationGuid;
                audit.CreatedDate = DateTime.Now;
                audit.TDateTime = DateTime.Now;
                _context.Add(audit);
                await _context.SaveChangesAsync();

                var application = await _context.Applications.FirstOrDefaultAsync(m => m.Id == new Guid(model.ApplicationId));
                application.UpdatedStaffNo = HttpContext.Session.GetString("StaffNo") == null ? "-" : HttpContext.Session.GetString("StaffNo");
                application.PaymentStatus = model.Status == true ? 1 : 0;
                application.Endosement = model.Endorsement == true ? 1 : 0;
                application.CertificateID = model.CertificateId;
                application.Remark = model.Remark == null ? "" : model.Remark;

                _context.Update(application);
                await _context.SaveChangesAsync();

                var jsonData = new { 
                    CertificateID = application.CertificateID,
                    PaymentStatus = application.PaymentStatus,
                    Endosement = application.Endosement,
                    Remark = application.Remark
                };
                return Json(jsonData);
            }
            catch (IOException e)
            {
                throw e;
            }

            /*}*/
            return RedirectToAction(nameof(Index));
        }

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,OrderNo,NoofPeople,ContactPhone,OverSeaOrDomestic,FromDestination,ToDestination,PremiumFee,TransitionID,Address,StartDate,EndDate,SecondContactPerson,SecondContactPhone,PaymentStatus,IsActive,IsDeleted,CreatedDate,CreatedStaffNo,UpdatedDate,UpdatedStaffNo")] Application application)
        {
            if (id != application.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(application);
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Applications == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Applications'  is null.");
            }
            var application = await _context.Applications.FindAsync(id);
            if (application != null)
            {
                _context.Applications.Remove(application);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ExportData()
        {
            //var applications = _context.Applications.ToList();

            var applications = from u in _context.Applications
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
                            certificateID = u.CertificateID == null ? "" : u.CertificateID
                        };

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Applications");

                // Add headers
                worksheet.Cells[1, 1].Value = "ApplicationID";
                worksheet.Cells[1, 2].Value = "NoofPeople";
                worksheet.Cells[1, 3].Value = "ContactPhone";
                worksheet.Cells[1, 4].Value = "Address";
                worksheet.Cells[1, 5].Value = "StartDate";
                worksheet.Cells[1, 6].Value = "EndDate";
                worksheet.Cells[1, 7].Value = "SecondContactPerson";
                worksheet.Cells[1, 8].Value = "SecondContactPhone";
                worksheet.Cells[1, 9].Value = "Status";
                worksheet.Cells[1,10].Value = "Merchant Name";

                // Add data rows
                int row = 2;
                foreach (var application in applications)
                {
                    worksheet.Cells[row, 1].Value = application.orderNo;
                    worksheet.Cells[row, 2].Value = application.noofPeople;
                    worksheet.Cells[row, 3].Value = application.contactPhone;
                    worksheet.Cells[row, 4].Value = application.address;
                    worksheet.Cells[row, 5].Value = application.startDate.ToString("yyyy-MM-dd");
                    worksheet.Cells[row, 6].Value = application.endDate.ToString("yyyy-MM-dd");
                    worksheet.Cells[row, 7].Value = application.secondContactPerson;
                    worksheet.Cells[row, 8].Value = application.secondContactPhone;
                    worksheet.Cells[row, 9].Value = application.paymentStatus;
                    worksheet.Cells[row, 10].Value = application.merchantname;

                    row++;
                }

                // Generate the Excel file bytes
                byte[] fileContents = package.GetAsByteArray();

                var fileName = $"Applications_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

                // Return the Excel file as a downloadable file
                return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }

        private bool ApplicationExists(Guid id)
        {
            return (_context.Applications?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
