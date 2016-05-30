namespace MugStore.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using ViewModels.Home;
    using Web.Controllers;
    using MugStore.Web.App_Start;

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

            if (model.Email.CompareTo("argirov@outlook.com") == 0 && model.Password.CompareTo("argirov") == 0)
            {
                this.Session.Add("logged_in", true);
            }

            return this.RedirectToAction("Index");
        }
    }
}
