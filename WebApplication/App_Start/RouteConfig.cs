using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: null,
                url: "",
                defaults: new { controller = "Books", action = "List", genre = (string)null }
            );

            routes.MapRoute(
                name: null,
                url: "{genre}",
                defaults: new { controller = "Books", action = "List"}
            );


            routes.MapRoute(
                name: null,
                url: "{controller}/{action}");

        }
    }
}
