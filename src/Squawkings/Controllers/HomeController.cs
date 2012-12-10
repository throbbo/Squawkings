﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
		private readonly IGravatarsHelper _gravatarHelper;

		public HomeController(IHomeDb homeDb, IGravatarsHelper gravatarHelper)
        {
        	_homeDb = homeDb;
			_gravatarHelper = gravatarHelper;
        }

    	public List<SquawkDisp> GetSquawkDisps()
        {
            var squawks = _homeDb.GetHomeSquawks(User.Identity.Id());

            return squawks;
        }


        [Authorize]
        public ActionResult Index()
        {
			var vm = new SquawkDispsViewModel { SquawkDisps = _gravatarHelper.SetUrls(GetSquawkDisps()) };
            return View(vm);
        }

        [Authorize]
		[HttpPost]
        public ActionResult Index(SqauwkDispsInputModel im)
        {
			if(string.IsNullOrEmpty(im.Content))
			{
				//ModelState.AddModelError("squawk","The field 'squawk' is required");
				var vm = new SquawkDispsViewModel { SquawkDisps = _gravatarHelper.SetUrls(GetSquawkDisps()) };
				return View("Index", vm);
			}

            var userId = User.Identity.Id();

            _homeDb.AddSquawk(userId, im.Content);

            return RedirectToAction("Index","Home");
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
		[StringLength(400, ErrorMessage = "You have entered text too long")]
		[Required]
        public string Content { get; set; }
    }
}