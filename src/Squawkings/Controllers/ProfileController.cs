using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Squawkings.Models;

namespace Squawkings.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogonDb _logonDb;

        public ProfileController() 
            : this(new LogonDb())
        {
            
        }
        public ProfileController(ILogonDb logonDb)
        {
            _logonDb = logonDb;
        }

        [Authorize]
        public ActionResult Index(string userName)
        {
            var user = _logonDb.GetUsers().FirstOrDefault(x => x.Username == userName) ?? new User();
            var vm = new ProfileViewModel()
                         {
                             Avatarurl = user.Avatarurl ?? "placeholder-profile-img.jpg",
                             Email = user.Email,
                             Firstname = user.Firstname,
                             Lastname = user.Lastname,
                             Username = userName
                         };
            return View(vm);
        }

    }
    public class ProfileViewModel
    {
        public int Userid { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Avatarurl { get; set; }
        public string Bio { get; set; }
    }
}
