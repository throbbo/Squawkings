using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NPoco;

namespace Squawkings.Controllers
{
    public class MenuController : Controller
    {
        private readonly IDatabase _db;

        public MenuController()
            : this(new Database("Squawkings"))
        {
            
        }

        public MenuController(IDatabase db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            var userName = _db.FirstOrDefault<string>("select UserName from dbo.Users where UserId = @0", User.Identity.Id());
            var menuVm = new MenuViewModel {UserName = userName};
            return PartialView("_menu", menuVm);
        }

    }

    public class MenuViewModel
    {
        public string UserName { get; set; }
    }
}
