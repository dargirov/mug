namespace MugStore.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Image",
                url: "Download/{name}",
                defaults: new { controller = "Image", action = "Index" },
                namespaces: new[] { "MugStore.Web.Controllers" }
            );

            routes.MapRoute(
                name: "ProductImage",
                url: "DownloadProductImage/{name}",
                defaults: new { controller = "Image", action = "ProductImage" },
                namespaces: new[] { "MugStore.Web.Controllers" }
            );

            routes.MapRoute(
                name: "GalleryCategory",
                url: "gallery/{acronym}",
                defaults: new { controller = "Gallery", action = "Category" },
                namespaces: new[] { "MugStore.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Tag",
                url: "tag/{acronym}",
                defaults: new { controller = "Home", action = "Tag" },
                namespaces: new[] { "MugStore.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Product",
                url: "p/{acronym}",
                defaults: new { controller = "Product", action = "Index" },
                namespaces: new[] { "MugStore.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MugStore.Web.Controllers" }
            );
        }
    }
}
