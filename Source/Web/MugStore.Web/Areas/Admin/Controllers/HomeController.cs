namespace MugStore.Web.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using App_Start;
    using Data.Models;
    using MugStore.Services.Common;
    using Services.Data;
    using ViewModels.Home;
    using Web.Controllers;

    public class HomeController : BaseController
    {
        private readonly IOrdersService orders;
        private readonly IBulletinsService bulletins;
        private readonly IImagesService images;
        private readonly IFeedbacksService feedbacks;
        private readonly ILoggerService logger;

        public HomeController(IOrdersService orders, IBulletinsService bulletin, IImagesService images, IFeedbacksService feedbacks, ILoggerService logger)
        {
            this.orders = orders;
            this.bulletins = bulletin;
            this.images = images;
            this.feedbacks = feedbacks;
            this.logger = logger;
        }

        [AuthorizeUser]
        public ActionResult Index()
        {
            var orders = this.orders.Get().Where(o => o.ConfirmationStatus != ConfirmationStatus.Denied).ToList();
            var bulletins = this.bulletins.Get().OrderByDescending(b => b.Id).ToList();
            var feedbacks = this.feedbacks.Get().OrderByDescending(x => x.IsNew).ThenByDescending(x => x.Id).ToList();
            var images = this.images.Get().OrderByDescending(i => i.Id).ToList();
            var priceChartOrders = new List<Order>();

            foreach (var order in this.orders.Get().Where(o => o.OrderStatus == OrderStatus.Finalized))
            {
                if (!priceChartOrders.Any(x => x.CreatedOn.ToShortDateString() == order.CreatedOn.ToShortDateString()))
                {
                    priceChartOrders.Add(order);
                }
            }

            var viewModel = new IndexViewModel()
            {
                Orders = orders,
                Bulletin = bulletins,
                Images = images,
                PriceChartOrders = priceChartOrders,
                Feedbacks = feedbacks,
                LogMessages = this.logger.GetLogMessages()
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
            var bulletins = this.bulletins.Get().OrderByDescending(b => b.Id).ToList();
            var viewModel = new BulletinViewModel()
            {
                Bulletins = bulletins
            };

            return this.View(viewModel);
        }

        [AuthorizeUser]
        [HttpPost]
        public ActionResult FeedbackStatus(int id)
        {
            var feedback = this.feedbacks.Get().Where(x => x.Id == id).FirstOrDefault();
            if (feedback == null)
            {
                throw new HttpException(404, id.ToString());
            }

            feedback.IsNew = false;
            this.feedbacks.Save();
            return null;
        }
    }
}
