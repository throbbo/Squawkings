using System;
using System.Collections.Generic;
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
            
            if(!IsUserNameValid(im.UserName))
            {
                ModelState.AddModelError("logonerror", LogonErrorMsg);
                return Index();
            }
            var hashPwFromDb = GetPasswordFromDb(im.UserName);                    
            //var hashPwFromInput = Crypto.HashPassword(im.PassWord);       
            if(Crypto.VerifyHashedPassword(hashPwFromDb, im.PassWord))
            {
                var isPersistant = im.RememberMe == "Y";
                FormsAuthentication.SetAuthCookie(im.UserName, isPersistant);
                ModelState.Clear();
                FormsAuthentication.RedirectFromLoginPage(im.UserName, isPersistant);
            }
            
            ModelState.AddModelError("logonerror", LogonErrorMsg);
            return Index();
        }

        private string GetPasswordFromDb(string userName)
        {
            // TODO: Setup Dependancy Injection
            ILogonDb logonDb = new LogonDb();
            var pw = logonDb.GetPasswordByUserName(userName);
            return pw; // "ACYvQX8NLtOlYY4LBDvOEcA9r1FrnBi9pR46MYyl/p0OhjqM8IOsksXb+o3pcbJN2g==";
        }

        private bool IsUserNameValid(string userName)
        {
            ILogonDb logonDb = new LogonDb();

            var isUserValid = logonDb.GetUsers().Any(x => x.Username == userName);
            return isUserValid;
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
