using System.Web.Mvc;
using System.Web.Routing;

namespace MyAccounts
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Skilltree_Filter",
                url: "Skilltree/{year}/{month}",
                defaults: new { controller = "Accounts", action = "Index", year = UrlParameter.Optional, month = UrlParameter.Optional },
                constraints: new
                {
                    year = @"(19[0-9]{2}|20[0-1][0-9])$",
                    month = @"(0[1-9]|1[0-2])$"
                }
            );

            routes.MapRoute(
                name: "Skilltree",
                url: "Skilltree/{action}/{id}",
                defaults: new { controller = "Accounts", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MyAccounts.Controllers" }
            );
        }
    }
}
