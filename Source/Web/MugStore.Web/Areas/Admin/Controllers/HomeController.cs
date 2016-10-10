namespace MugStore.Web.Areas.Admin.Controllers
{
    using System.Configuration;
    using System.Linq;
    using System.Web.Mvc;
    using App_Start;
    using Data.Models;
    using Services.Data;
    using ViewModels.Home;
    using Web.Controllers;

    public class HomeController : BaseController
    {
        private readonly IOrdersService orders;
        private readonly IBulletinsService bulletin;
        private readonly IImagesService images;

        public HomeController(IOrdersService orders, IBulletinsService bulletin, IImagesService images)
        {
            this.orders = orders;
            this.bulletin = bulletin;
            this.images = images;
        }

        [AuthorizeUser]
        public ActionResult Index()
        {
            var orders = this.orders.Get().Where(o => o.ConfirmationStatus != ConfirmationStatus.Denied).ToList();
            var bulletins = this.bulletin.Get().OrderByDescending(b => b.Id).ToList();
            var images = this.images.Get().OrderByDescending(i => i.Id).ToList();

            var viewModel = new IndexViewModel()
            {
                Orders = orders,
                Bulletin = bulletins,
                Images = images
            };

            return this.View(viewModel);
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
