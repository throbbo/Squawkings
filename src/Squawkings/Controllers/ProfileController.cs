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
        private readonly ISquawksDb _squawksDb;

        public ProfileController() 
            : this(new SquawksDb())
        {
            
        }
        public ProfileController(ISquawksDb squawksDb)
        {
            _squawksDb = squawksDb;
        }

        public ActionResult Index(string userName)
        {
            var vm = new ProfileViewModel()
                         {
                             SquawkDisps = _squawksDb.GetProfileSquawks(userName),
                             ProfileDetails = _squawksDb.UserDispGetProfileByUserName(userName)
                         };
            return View(vm);
        }

    }
    
    public class ProfileViewModel
    {
        public ProfileViewModel()
        {
            SquawkDisps = new List<SquawkDisp>();
        }
        public List<SquawkDisp> SquawkDisps { get; set; }
        public UserDisp ProfileDetails { get; set; }
    }

    public class ProfileInputModel
    {
        public int Userid { get; set; }
        public int FollowUserid { get; set; }
    }
}
