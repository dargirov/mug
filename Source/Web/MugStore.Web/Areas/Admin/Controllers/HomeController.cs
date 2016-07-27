namespace MugStore.Web.Areas.Admin.Controllers
{
    using System.Configuration;
    using System.Linq;
    using System.Web.Mvc;
    using App_Start;
    using Services.Data;
    using ViewModels.Home;
    using Web.Controllers;

    public class HomeController : BaseController
    {
        private readonly IBulletinsService bulletin;

        public HomeController(IBulletinsService bulletin)
        {
            this.bulletin = bulletin;
        }

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

        [AuthorizeUser]
        public ActionResult Bulletin()
        {
            var bulletins = this.bulletin.Get().OrderByDescending(b => b.Id).ToList();
            var viewModel = new BulletinViewModel()
            {
                Bulletins = bulletins
            };

            return this.View(viewModel);
        }
    }
}
