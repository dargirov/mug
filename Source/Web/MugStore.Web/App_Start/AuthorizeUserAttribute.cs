namespace MugStore.Web.App_Start
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var logged = filterContext.HttpContext.Session["logged_in"];
            if (logged == null || !(bool)logged)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Login" }));
            }
        }
    }
}
