using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TravelInsurance.Infrastructure.Dto.Travellers;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Repository.Ef;


namespace TravelInsuranceWebSite.Controllers
{
    public class TravellersController : Controller
    {
        #region Variables
        private readonly ILogger<TravellersController> _logger;
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constructor
        public TravellersController(ILogger<TravellersController> logger, ApplicationDbContext context)
        {

            _logger = logger;
            _context = context;
        }
        #endregion

        #region Helper
        public IActionResult CheckPermission()
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

        #endregion

        #region Methods
        public async Task<IActionResult> Index(Guid? id)
        {
            CheckPermission();

            var traveller = await (from u in _context.Travellers
                                   select new TravellersRequestViewModel
                                   {
                                       Id = u.Id,
                                       OrderId = u.OrderId,
                                       Name = u.Name,
                                       NRCOrPassportData = u.NRCType != 0 ? u.NRCType.ToString() + "/" + u.NRCOrPassportData : u.NRCOrPassportData,
                                       Age = u.Age
                                   }
                            ).Where(m => m.OrderId == id).ToListAsync();


            if (traveller == null)
            {
                return NotFound();
            }
            return View(traveller);
            //return View();
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Travellers == null)
            {
                return NotFound();
            }

            var traveller = await _context.Travellers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (traveller == null)
            {
                return NotFound();
            }

            return View(traveller);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderId,Name,NRCType,NRCOrPassportData,Age")] Traveller traveller)
        {
            if (ModelState.IsValid)
            {
                traveller.Id = Guid.NewGuid();
                _context.Add(traveller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(traveller);
        }

        // GET: Travellers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Travellers == null)
            {
                return NotFound();
            }

            //var traveller = await _context.Travellers.FindAsync(id);
            var traveller = await _context.Travellers
               .FirstOrDefaultAsync(m => m.OrderId == id);
            if (traveller == null)
            {
                return NotFound();
            }
            return View(traveller);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,OrderId,Name,NRCType,NRCOrPassportData,Age")] Traveller traveller)
        {
            if (id != traveller.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(traveller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravellerExists(traveller.Id))
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
            return View(traveller);
        }

        // GET: Travellers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Travellers == null)
            {
                return NotFound();
            }

            var traveller = await _context.Travellers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (traveller == null)
            {
                return NotFound();
            }

            return View(traveller);
        }

        // POST: Travellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Travellers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Travellers'  is null.");
            }
            var traveller = await _context.Travellers.FindAsync(id);
            if (traveller != null)
            {
                _context.Travellers.Remove(traveller);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TravellerExists(Guid id)
        {
            return (_context.Travellers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        #endregion

    }
}
