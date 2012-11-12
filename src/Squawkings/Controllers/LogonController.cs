using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using NPoco;
using Squawkings.Models;

namespace Squawkings.Controllers
{
    public class LogonController : Controller
    {
        private readonly ILogonDb _logonDb;

        public LogonController() 
            : this(new LogonDb())
        {
        }
        public LogonController(ILogonDb logonDb)
        {
            _logonDb = logonDb;
        }

        private const string LogonErrorMsg = "Your Logon attempt was not successful!";

        [HttpGet]
        public ActionResult Index(string returnUrl)
        {
            var vm = new LogonViewModel{ReturnUrl = returnUrl};
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken(Salt = "AntiForgeryTokenSalt")]
        public ActionResult Index(LogonInputModel im)
        {
            if (!ModelState.IsValid)
                return Index(im.ReturnUrl);

            var hashPwFromDb = _logonDb.GetPasswordByUserName(im.UserName);                    
            if(hashPwFromDb != null && Crypto.VerifyHashedPassword(hashPwFromDb, im.PassWord))
            {
                FormsAuthentication.SetAuthCookie(im.UserName, im.RememberMe);
                if(!string.IsNullOrEmpty(im.ReturnUrl))
                    return Redirect(im.ReturnUrl);

                return RedirectToAction("Index", "Home");  
            }
            
            ModelState.AddModelError("", LogonErrorMsg);
            return Index(im.ReturnUrl);
        }

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
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PassWord { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
    public class LogonViewModel    
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
