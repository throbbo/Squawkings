using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NPoco;
using Squawkings.Models;

namespace Squawkings.Controllers
{
    public class UploadFileController : Controller
    {
        private readonly IDatabase _db;

        public UploadFileController(IDatabase db)
        {
            _db = db;
        }

        [Authorize]
        public ActionResult Index()
        {
        	var vm = new UploadFileViewModel();
			return View(vm);
        }

		[Authorize]
		public ActionResult Cancel(UploadFileInputModel im)
		{
			return RedirectToAction("Index", "Profile", new { username = _db.SingleById<User>(User.Identity.Id()).UserName });
		}

        [Authorize]
        [HttpPost]
        public ActionResult UploadFile(UploadFileInputModel im)
        {
			if ((im.File == null || string.IsNullOrEmpty(im.File.FileName)) && !im.IsGravatar)
            {
                ModelState.AddModelError("", "You must select a valid Image or be using a Gravatar to Update Changes!");
				return RedirectToAction("LoggedInProfile", "Profile");
            }
			
			var user = _db.SingleById<User>(User.Identity.Id());

			if(!im.IsGravatar) {
				var file = im.File;

				var fileNameUrl = string.Format("dev_images/{0}_d.jpg", user.UserName );

				if (string.IsNullOrEmpty(fileNameUrl)) {
					ModelState.AddModelError("", "You must select a valid Image Url!");
					return RedirectToAction("LoggedInProfile", "Profile");
				}

				file.SaveAs(Request.MapPath("~/Content/"+fileNameUrl));
				user.AvatarUrl = fileNameUrl;
			}
            user.IsGravatar = im.IsGravatar;

            _db.Update(user);
            return RedirectToAction("Index", "Profile", new {username = user.UserName});
        }
    }

    public class UploadFileViewModel
    {
    	public UploadFileViewModel()
    	{
    		HeaderView = new HeaderView {Header = "Profile Upload", Description = "Use a gravatar or Upload an image"};
    	}
        public HttpPostedFileBase File { get; set; } 
        public bool IsGravatar { get; set; }
    	public HeaderView HeaderView { get; set; }
    }

	public class HeaderView
	{
		public string Header { get; set; }
		public string Description { get; set; }
	}

	public class UploadFileInputModel
    {
        public HttpPostedFileBase File { get; set; } 
        public bool IsGravatar { get; set; } 
    }
}
