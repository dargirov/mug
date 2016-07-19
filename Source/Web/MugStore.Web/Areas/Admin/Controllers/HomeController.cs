namespace MugStore.Web.Areas.Admin.Controllers
{
    using System.Configuration;
    using System.Web.Mvc;
    using App_Start;
    using ViewModels.Home;
    using Web.Controllers;
    using MugStore.Services.Data;

    public class HomeController : BaseController
    {
        [AuthorizeUser]
        public ActionResult Index()
        {
            return this.View();
        }

        [AuthorizeUser]
        public ActionResult Logout()
        {
            this.Session.Remove("logged_in");
            this.Session.RemoveAll();
            return this.RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return this.PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Index");
            }

            var email = ConfigurationManager.AppSettings["AdminLoginEmail"];
            var pass = ConfigurationManager.AppSettings["AdminLoginPassword"];

            if (model.Email.CompareTo(email) == 0 && model.Password.CompareTo(pass) == 0)
            {
                this.Session.Add("logged_in", true);
            }

            return this.RedirectToAction("Index");
        }
    }
}
