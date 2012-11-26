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

        public UploadFileController()
            : this(new Database("Squawkings"))
        {
            
        } 
        public UploadFileController(IDatabase db)
        {
            _db = db;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult UploadFile(UploadFileInputModel im)
        {
            var file = im.File;
            var user = _db.SingleById<User>(User.Identity.Id());
            var fileNameUrl = string.Format("dev_images/{0}_d.jpg", user.Userid );

            file.SaveAs(Request.MapPath("~/Content/"+fileNameUrl));
            user.AvatarUrl = fileNameUrl;
            user.IsGravatar = im.IsGravatar;

            _db.Update(user);
            return RedirectToAction("Index", "Profile", new {username = user.UserName});
        }
    }

    public class UploadFileViewModel
    {

        public HttpPostedFileBase File { get; set; } 
        public bool IsGravatar { get; set; } 
    }
    public class UploadFileInputModel
    {
        public HttpPostedFileBase File { get; set; } 
        public bool IsGravatar { get; set; } 
    }
}
