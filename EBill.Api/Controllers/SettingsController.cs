using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using TravelInsurance.Infrastructure.Dto.Permission;
using TravelInsurance.Infrastructure.Dto.Settings;
using TravelInsurance.Infrastructure.Dto.TransactionLogs;
using TravelInsurance.Infrastructure.Dto.User;
using TravelInsurance.Infrastructure.Entities;
using TravelInsurance.Infrastructure.IServices;
using TravelInsurance.Repository.Ef;
using TravelInsuranceWebSite.Api.ViewModels;
using TravelInsuranceWebSite.ViewModels;

namespace WebApplication5.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;
        private readonly ISettingsService _settingsService;
        private readonly IMapper _mapper;

        public SettingsController(ISettingsService settingsService,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment, 
            ApplicationDbContext context, IMapper mapper)
        {
            _settingsService = settingsService;
            Environment = _environment;
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }
            var settings = _context.Settings.FirstOrDefault();
            return View(_mapper.Map<SettingsRequestViewModel>(settings));
        }

        #region Helper
        public bool CheckPermission()
        {
            string staffname = HttpContext.Session.GetString("StaffNo");
            if (staffname == null)
            {
                return false;
            }

            if (CheckExist(staffname) == 0)
            {

                return false;
            }

            return true;
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
        /*  public int CheckExist(string StaffNo)
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
          }*/

    

        public async Task<IActionResult> MobileBanner(Guid id)
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var speaker = await _context.Settings
                    .FirstOrDefaultAsync(m => m.Id == id);

               /* var speakerViewModel = new SettingsViewModel()
                {
                    Name = speaker.MobileBanner
                };*/

                if (speaker == null)
                {
                    return NotFound();
                }
                return View(speaker);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> MobileBanner(List<IFormFile> postedFiles, Guid id)
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }

            if (postedFiles.Count == 0)
            {
                var speakercount = await _context.Settings
                  .FirstOrDefaultAsync(m => m.Id == id);


                if (speakercount == null)
                {
                    return NotFound();
                }

                ViewBag.ErrorMessage = "Please select a file.";
                return View(speakercount);
            }
            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;

            SettingsRequestViewModel settings = new SettingsRequestViewModel();

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
                    settings.MobileBanner = fileName;
                    ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
                }
            }
            try
            {
                settings.Id = id;
                await _settingsService.UpdateMobileBannerAsync(_mapper.Map<SettingsRequestViewModel>(settings));

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }

            var speaker = await _context.Settings
                    .FirstOrDefaultAsync(m => m.Id == id);


            return View(speaker);
        }

        public async Task<IActionResult> TabletBanner(Guid id)
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var speaker = await _context.Settings
                    .FirstOrDefaultAsync(m => m.Id == id);


                if (speaker == null)
                {
                    return NotFound();
                }
                return View(speaker);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> TabletBanner(List<IFormFile> postedFiles, Guid id)
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }

            if (postedFiles.Count == 0)
            {
                var speakercount = await _context.Settings
                  .FirstOrDefaultAsync(m => m.Id == id);


                if (speakercount == null)
                {
                    return NotFound();
                }

                ViewBag.ErrorMessage = "Please select a file.";
                return View(speakercount);
            }
            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;
            SettingsRequestViewModel settings = new SettingsRequestViewModel();

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
                    settings.TabletBanner = fileName;
                    ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
                }
            }

            try
            {
                settings.Id = id;
                await _settingsService.UpdateTabletBannerAsync(_mapper.Map<SettingsRequestViewModel>(settings));

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }

         
            var speaker = await _context.Settings
                    .FirstOrDefaultAsync(m => m.Id == id);


            return View(speaker);
        }

        public async Task<IActionResult> Banner(Guid id)
        {
            CheckPermission();
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var speaker = await _context.Settings
                    .FirstOrDefaultAsync(m => m.Id == id);

              /*  var speakerViewModel = new SettingsViewModel()
                {
                    Name = speaker.Banner
                };*/

                if (speaker == null)
                {
                    return NotFound();
                }
                return View(speaker);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Banner(List<IFormFile> postedFiles, Guid id)
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }

            if (postedFiles.Count == 0)
            {
                var speakercount = await _context.Settings
                  .FirstOrDefaultAsync(m => m.Id == id);


                if (speakercount == null)
                {
                    return NotFound();
                }

                ViewBag.ErrorMessage = "Please select a file.";
                return View(speakercount);
            }

            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;

            SettingsRequestViewModel settings = new SettingsRequestViewModel();
           

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
                    settings.Banner = fileName;
                    ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
                }
            }

            try
            {
                settings.Id = id;
                await _settingsService.UpdateDesktopBannerAsync(_mapper.Map<SettingsRequestViewModel>(settings));

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }
            var speaker = await _context.Settings
                    .FirstOrDefaultAsync(m => m.Id == id);

            
            return View(speaker);
        }

        public async Task<IActionResult> PopupBanner(Guid id)
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var speaker = await _context.Settings
                    .FirstOrDefaultAsync(m => m.Id == id);


                if (speaker == null)
                {
                    return NotFound();
                }
                return View(speaker);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> PopupBanner(List<IFormFile> postedFiles, Guid id)
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }

            if (postedFiles.Count == 0)
            {
                var speakercount = await _context.Settings
                  .FirstOrDefaultAsync(m => m.Id == id);


                if (speakercount == null)
                {
                    return NotFound();
                }

                ViewBag.ErrorMessage = "Please select a file.";
                return View(speakercount);
            }
            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;
            SettingsRequestViewModel settings = new SettingsRequestViewModel();

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
                    settings.PopupBanner = fileName;
                    ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
                }
            }

            try
            {
                settings.Id = id;
                await _settingsService.UpdatePopupBannerAsync(_mapper.Map<SettingsRequestViewModel>(settings));

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }


            var speaker = await _context.Settings
                    .FirstOrDefaultAsync(m => m.Id == id);


            return View(speaker);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SettingsRequestViewModel settings)
        {
            if (CheckPermission() == false)
            {
                return RedirectToAction("Index", "Login");
            }

            try
            {
                await _settingsService.UpdateAsync(_mapper.Map<SettingsRequestViewModel>(settings));

            }
            catch (DbUpdateConcurrencyException)
            {
                    throw;
               
            }

            return RedirectToAction("Index");
        }

   
    }
}
