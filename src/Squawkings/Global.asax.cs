using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SchoStack.Web.Conventions;
using SchoStack.Web.Conventions.Core;

namespace Squawkings
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");
            
            routes.MapRoute("Home",   "Home/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional } );
            routes.MapRoute("Logon",  "Logon/{action}/{id}", new { controller = "Logon", action = "Index", id = UrlParameter.Optional } );
            routes.MapRoute("Global", "Global/{action}/{id}", new { controller = "Global", action = "Index", id = UrlParameter.Optional} );

            routes.MapRoute(
                "Profile", // Route name
                "{username}", // URL with parameters
                new { controller = "Profile", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            HtmlConventionFactory.Add(new DefaultHtmlConventions());
            HtmlConventionFactory.Add(new DataAnnotationHtmlConventions());
            HtmlConventionFactory.Add(new DataAnnotationValidationHtmlConventions());
        }
    }
}