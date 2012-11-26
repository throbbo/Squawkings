using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NPoco;
using Squawkings.Models;

namespace Squawkings
{
    public class FooterController : Controller
    {
        private readonly IDatabase _db;

        public FooterController()
            : this(new Database("Squawkings"))
        {
            
        }
        public FooterController(IDatabase db)
        {
            _db = db;
        }

        [OutputCache(Duration = 10, VaryByParam = "*")]
        public ActionResult Index()
        {
            var pageStats = _db.Single<FooterView>(@"select (select count(*) from dbo.Users) TotalUsers, 
    (select count(*) from dbo.Squawks) TotalSquawks
");

            return PartialView("_footer", pageStats);
        }
    }
}