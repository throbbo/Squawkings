using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Squawkings.Models;

namespace Squawkings.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeDb _homeDb;

        public HomeController() 
            : this(new HomeDb())
        {
        }
        public HomeController(IHomeDb homeDb)
        {
            _homeDb = homeDb;
        }

        public List<SquawkDisp> GetSquawkDisps()
        {
            var squawks = _homeDb.GetHomeSquawks(User.Identity.Id());
            return squawks;
        }

        [Authorize]
        public ActionResult Index()
        {
            var vm = new SquawkDispsViewModel {SquawkDisps = GetSquawkDisps()};
            return View(vm);
        }

        [Authorize]
        public ActionResult Squawk(SqauwkDispsInputModel im)
        {
            var userId = User.Identity.Id();

            _homeDb.AddSquawk(userId, im.Content);

            return RedirectToAction("index");
        }
    }

    public class SquawkDispsViewModel
    {
        public SquawkDispsViewModel()
        {
            OtherStuff = "Hello World";
            SquawkDisps = new List<SquawkDisp>();
        }
        public List<SquawkDisp> SquawkDisps { get; set; }
        public string OtherStuff { get; set; }
    }

    public class SqauwkDispsInputModel
    {
        public string Content { get; set; }
    }
}