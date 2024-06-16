using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TravelInsurance.Infrastructure.Dto.User;
using TravelInsurance.Repository.Ef;



namespace TravelInsuranceWebSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class loginApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public loginApiController(ApplicationDbContext context)
        {
            _context = context;
        }

      

        [HttpPost("checkForLogIn")]
        public IActionResult CheckForLogIn([FromBody] MyModel4 par)
        {
            var model = new StaffModel();

            //List<StaffModel> lst = dstaff.CheckForLogIn(username, password);
            List<StaffModel> lst = (from staff in _context.Users
                                    join permi in _context.Permission on staff.UserName equals permi.staffno
                                    join role in _context.Roles on permi.roleid equals role.Id
                                    where staff.UserName == par.username && staff.PasswordHash == par.password
                                    select new StaffModel
                                    {
                                        staffname = staff.UserName,
                                    }).ToList();

            if (lst.Count > 0)
            {
                model.MessageType = 1;

                //TMN
                /*   Session["Role"] = lst[0].positionID;
                   //TMN
                   Session["StaffID"] = lst[0].StaffID;
                   Session["StaffNo"] = lst[0].staffno;
                   Session["StaffName"] = lst[0].staffname;
                   Session["locationdescription"] = lst[0].locationdescription;
                   Session["locationcode"] = lst[0].locationcode.ToUpper().Trim();*/
            }
            else
            {
                 model.MessageType = 2;
                 model.Message = "Invalid Username or Password.";
            }
            var jsonData = new {  data = model };
            return Ok(jsonData);
        }

    
        public class MyModel4
        {
            public string username { get; set; }
            public string password { get; set; }
        }
    }
}
