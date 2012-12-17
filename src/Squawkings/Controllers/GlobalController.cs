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
		private readonly IGravatarsHelper _gravatarHelper;

		public GlobalController(IGlobalDb globalDb, IGravatarsHelper gravatarHelper)
		{
			_globalDb = globalDb;
			_gravatarHelper = gravatarHelper;
		}

		public ActionResult Index()
    	{
			var vm = new GlobalViewModel { SquawkDisps = _gravatarHelper.SetUrls(_globalDb.GetGlobalSquawks()) };
            return View(vm);
        }

    }

	public class GlobalViewModel
	{
		public GlobalViewModel()
		{
			HeaderView = new HeaderView {Header = "Global Squawks", Description = "See everything happening right now"};
		}
		public List<SquawkDisp> SquawkDisps { get; set; }
		public string OtherStuff { get; set; }
		public HeaderView HeaderView { get; set; }
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

}
