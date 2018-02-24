namespace MugStore.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using System.Xml.Linq;
    using Common;
    using MugStore.Data.Models;
    using Services.Data;
    using ViewModels.Home;

    [RoutePrefix("")]
    public class HomeController : BaseController
    {
        private readonly IProductsService products;
        private readonly ICitiesService cities;
        private readonly ITagsService tags;
        private readonly ICategoriesService categories;
        private readonly ICouriersService couriers;
        private readonly IFeedbacksService feedbacks;
        private readonly IOrdersService orders;
        private readonly IBulletinsService bulletins;
        private readonly IBlogService blog;

        public HomeController(IProductsService products, ICitiesService cities, ITagsService tags, ICategoriesService categories, ICouriersService couriers, IFeedbacksService feedbacks, IOrdersService orders, IBulletinsService bulletins, IBlogService blog)
        {
            this.products = products;
            this.cities = cities;
            this.tags = tags;
            this.categories = categories;
            this.couriers = couriers;
            this.feedbacks = feedbacks;
            this.orders = orders;
            this.bulletins = bulletins;
            this.blog = blog;
        }

        private IList<string> MugInfoTypes => new List<string>() { "dark", "white" };

        public ActionResult Index()
        {
            var products = this.products.Get().Where(c => c.Active).Include(p => p.Images).OrderByDescending(p => p.Id).Take(GlobalConstants.MaxProductsInHomePage).ToList();
            this.ViewBag.Cities = this.cities.Get().Where(c => c.Highlight).OrderBy(x => x.Name).ToList();
            this.ViewBag.Couriers = this.couriers.Get().Where(c => c.Active).OrderBy(c => c.Name).ToList();
            this.ViewBag.ShowRight = true;
            this.ViewBag.SingleMugPrice = decimal.Parse(ConfigurationManager.AppSettings["SingleMugPrice"]);
            this.ViewBag.SingleMugMsrpPrice = decimal.Parse(ConfigurationManager.AppSettings["SingleMugMsrpPrice"]);
            this.ViewBag.Decrease = Math.Round((this.ViewBag.SingleMugMsrpPrice - this.ViewBag.SingleMugPrice) / this.ViewBag.SingleMugMsrpPrice * 100);
            this.ViewBag.DeliveryPrice = decimal.Parse(ConfigurationManager.AppSettings["DeliveryPrice"]);
            this.ViewBag.PageDescription = "С този сайт може сам да си направиш чаша. Качи до 3 снимки и ги разположи на желаното място върху 3D модел на чаша. Поръчката става бързо и не е необходима регистрация.";
            this.AddTagsToViewBag(this.tags);

            this.ViewBag.HideGoogleAnalytics = false;
            var ping = this.Request.QueryString["ping"];
            if (ping != null && ping.CompareTo(GlobalConstants.PingParam) == 0)
            {
                this.ViewBag.HideGoogleAnalytics = true;
            }

            var viewModel = new IndexViewModel()
            {
                Products = products,
                MugInfoType = this.MugInfoTypes.OrderBy(x => Guid.NewGuid()).First(),
                BlogPosts = this.blog.GetPosts().ToList()
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult Contacts()
        {
            this.AddTagsToViewBag(this.tags);
            this.ViewBag.PageDescription = "За контакти и въпроси при направа на чаша може да се свържете с нас.";

            var viewModel = new ContactsViewModel()
            {
                Email = ConfigurationManager.AppSettings["ContactsEmail"],
                Phone = ConfigurationManager.AppSettings["ContactsPhone"]
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contacts(ContactsInputModel model)
        {
            this.AddTagsToViewBag(this.tags);
            this.ViewBag.PageDescription = "За контакти и въпроси при направа на чаша може да се свържете с нас.";

            if (this.ModelState.IsValid)
            {
                this.feedbacks.Add(new Data.Models.Feedback()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Text = model.Comment,
                    IsNew = true
                });

                this.ViewBag.MailSend = true;
            }
            else
            {
                this.ViewBag.MailSend = false;
            }

            var viewModel = new ContactsViewModel()
            {
                Email = ConfigurationManager.AppSettings["ContactsEmail"],
                Phone = ConfigurationManager.AppSettings["ContactsPhone"]
            };

            return this.View(viewModel);
        }

        public ActionResult ImageHelp()
        {
            return this.View();
        }

        [Route("s")]
        public ActionResult Status()
        {
            var orders = this.orders.Get().Where(x => (x.ConfirmationStatus == ConfirmationStatus.Pending || x.ConfirmationStatus == ConfirmationStatus.Confirmed) && x.OrderStatus == OrderStatus.InProgress).Count();
            var feedbacks = this.feedbacks.Get().Where(x => x.IsNew).Count();
            var bulletins = this.bulletins.Get().Count();

            return this.Content($"{orders}:{feedbacks}:{bulletins}", "text/plain", Encoding.UTF8);
        }

        [Route("sitemap.xml")]
        public ActionResult SitemapXml()
        {
            var nodes = new List<SitemapNode>();

            nodes.Add(new SitemapNode() { Priority = 1, Url = this.Url.RouteUrl("Default", new { action = "Index" }, this.Request.Url.Scheme) });
            nodes.Add(new SitemapNode() { Priority = 0.5, Url = this.Url.RouteUrl("Default", new { action = "Contacts" }, this.Request.Url.Scheme) });
            nodes.Add(new SitemapNode() { Priority = 0.6, Url = this.Url.RouteUrl("Default", new { controller = "Gallery", action = "Index" }, this.Request.Url.Scheme) });

            var products = this.products.Get().Where(p => p.Active).OrderByDescending(p => p.Id).ToList();
            foreach (var product in products)
            {
                nodes.Add(new SitemapNode()
                    {
                        Priority = 0.8,
                        Frequency = SitemapFrequency.Monthly,
                        Url = this.Url.RouteUrl("Product", new { acronym = product.Acronym }, this.Request.Url.Scheme)
                    });
            }

            var categories = this.categories.Get().OrderBy(c => c.Order).ToList();
            foreach (var category in categories)
            {
                nodes.Add(new SitemapNode()
                {
                    Priority = 0.6,
                    Frequency = SitemapFrequency.Weekly,
                    Url = this.Url.RouteUrl("GalleryCategory", new { acronym = category.Acronym }, this.Request.Url.Scheme)
                });
            }

            var xml = this.GetSitemapDocument(nodes);
            return this.Content(xml, "application/xml", Encoding.UTF8);
        }

        private string GetSitemapDocument(IEnumerable<SitemapNode> sitemapNodes)
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement root = new XElement(xmlns + "urlset");

            foreach (SitemapNode sitemapNode in sitemapNodes)
            {
                XElement urlElement = new XElement(
                    xmlns + "url",
                    new XElement(xmlns + "loc", sitemapNode.Url),
                    sitemapNode.LastModified == null ? null : new XElement(
                        xmlns + "lastmod",
                        sitemapNode.LastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                    sitemapNode.Frequency == null ? null : new XElement(
                        xmlns + "changefreq",
                        sitemapNode.Frequency.Value.ToString().ToLowerInvariant()),
                    sitemapNode.Priority == null ? null : new XElement(
                        xmlns + "priority",
                        sitemapNode.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)));
                root.Add(urlElement);
            }

            XDocument document = new XDocument(root);
            return document.ToString();
        }
    }
}
