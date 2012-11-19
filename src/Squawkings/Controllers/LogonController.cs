using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
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
                return ReturnLogonError(im.ReturnUrl);

            var user = _logonDb.GetUsers().FirstOrDefault(x=>x.Username==im.UserName); 
            if(user==null) 
                return ReturnLogonError(im.ReturnUrl);

            var pw = _logonDb.GetPasswordByUserName(im.UserName);
            if(string.IsNullOrEmpty(pw)) 
                return ReturnLogonError(im.ReturnUrl); 
                
            if(Crypto.VerifyHashedPassword(pw, im.PassWord))
            {
                FormsAuthentication.SetAuthCookie(user.Userid.ToString(CultureInfo.InvariantCulture), im.RememberMe);
                if(!string.IsNullOrEmpty(im.ReturnUrl))
                    return Redirect(im.ReturnUrl);

                return RedirectToAction("Index", "Home");  
            }
            
            return ReturnLogonError(im.ReturnUrl);
        }

        private ActionResult ReturnLogonError(string returnUrl)
        {
            ModelState.AddModelError("", LogonErrorMsg);
            return Index(returnUrl);
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
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        public bool RememberMe { get; set; }
        [HiddenInput]
        public string ReturnUrl { get; set; }
    }
}
