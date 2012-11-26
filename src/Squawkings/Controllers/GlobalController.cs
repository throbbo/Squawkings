using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Squawkings.Models;

namespace Squawkings.Controllers
{
    public class GlobalController : Controller
    {
        private readonly IGlobalDb _globalDb;

        public GlobalController() 
            : this(new GlobalDb())
        {
        }
        public GlobalController(IGlobalDb globalDb)
        {
            _globalDb = globalDb;
        }

        [Authorize]
        public ActionResult Index()
        {
            var vm = new GlobalViewModel { SquawkDisps = _globalDb.GetGlobalSquawks() };
            return View(vm);
        }

    }

    public class GlobalInputModel
    {
        public GlobalInputModel()
        {
            OtherStuff = "Hello World";
            SquawkDisps = new List<SquawkDisp>();
        }
        public List<SquawkDisp> SquawkDisps { get; set; }
        public string OtherStuff { get; set; }
    }
    public class GlobalViewModel
    {
        public List<SquawkDisp> SquawkDisps { get; set; }
        public string OtherStuff { get; set; }
    }

}
