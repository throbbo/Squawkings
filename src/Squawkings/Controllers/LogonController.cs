using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace Squawkings.Controllers
{
    public class LogonController : Controller
    {
        //
        // GET: /Logon/
        private const string LogonErrorMsg = "Your Logon attempt was not successful!";
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken(Salt = "AntiForgeryTokenSalt")]
        public ActionResult Index(LogonInputModel im)
        {
            if(string.IsNullOrEmpty(im.PassWord) || string.IsNullOrEmpty(im.UserName))
            {
                ModelState.AddModelError("logonerror", LogonErrorMsg);
                return Index();
            }
            
            if(im.UserName == "test")
            {
                var hashPwFromDb = Crypto.HashPassword("test");
                
                if(Crypto.VerifyHashedPassword(hashPwFromDb, im.UserName))
                {
                    var isPersistant = im.RememberMe == "Y";
                    FormsAuthentication.SetAuthCookie(im.UserName, isPersistant);
                    ModelState.Clear();
                    FormsAuthentication.RedirectFromLoginPage(im.UserName, isPersistant);
                }
            }
            
            ModelState.AddModelError("logonerror", LogonErrorMsg);
            return Index();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Logoff()
        {
            var x = Request.IsAuthenticated;
            var y = User.Identity.IsAuthenticated;

            FormsAuthentication.SignOut();
            Session.Abandon();

            return RedirectToAction("Index");
        }
    }

    public class LogonInputModel    
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string RememberMe { get; set; }
    }
}
