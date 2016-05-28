namespace MugStore.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using ViewModels.Home;
    using Web.Controllers;

    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var loggedIn = this.Session["logged_in"];

            if (loggedIn == null || !(bool)loggedIn)
            {
                return this.RedirectToAction("Login");
            }

            return this.View();
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

            if (model.Email.CompareTo("argirov@outlook.com") == 0 && model.Password.CompareTo("argirov") == 0)
            {
                this.Session.Add("logged_in", true);
            }

            return this.RedirectToAction("Index");
        }
    }
}
