namespace MugStore.Web.Controllers
{
    using System;
    using System.Configuration;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Net.Mime;
    using System.Text;
    using System.Web.Mvc;
    using System.Xml.Linq;
    using Services.Data;
    using ViewModels.Home;
    using MugStore.Common;

    [RoutePrefix("")]
    public class HomeController : BaseController
    {
        private readonly IProductsService products;
        private readonly ICitiesService cities;
        private readonly ITagsService tags;
        private readonly ICategoriesService categories;

        public HomeController(IProductsService products, ICitiesService cities, ITagsService tags, ICategoriesService categories)
        {
            this.products = products;
            this.cities = cities;
            this.tags = tags;
            this.categories = categories;
        }

        public ActionResult Index()
        {
            var products = this.products.Get().Where(c => c.Active).Include(p => p.Images).OrderByDescending(p => p.Id).Take(GlobalConstants.MaxProductsInHomePage).ToList();
            this.ViewBag.Cities = this.cities.Get().Where(c => c.Highlight).ToList();
            this.ViewBag.ShowRight = true;
            this.ViewBag.SingleMugPrice = GlobalConstants.SingleMugPrice;
            this.ViewBag.DeliveryPrice = GlobalConstants.DeliveryPrice;
            this.AddTagsToViewBag(this.tags);

            var viewModel = new IndexViewModel()
            {
                Products = products
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult Contacts()
        {
            this.AddTagsToViewBag(this.tags);
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contacts(ContactsInputModel model)
        {
            this.AddTagsToViewBag(this.tags);

            if (this.ModelState.IsValid)
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress("argirov@outlook.com"));
                message.From = new MailAddress("info@mug3.eu");
                message.Subject = "Съобщение от mug3.eu";
                message.Body = string.Format("Name: {0}<br>Email: {1}<br><hr>Comment: {2}", model.Name, model.Email, model.Comment);
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    var email = ConfigurationManager.AppSettings["ContactsEmailFrom"];
                    var password = ConfigurationManager.AppSettings["ContactsEmailPassword"];

                    var credential = new NetworkCredential
                    {
                        UserName = email,
                        Password = password
                    };

                    smtp.Credentials = credential;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Send(message);
                    this.ViewBag.MailSend = true;
                }
            }
            else
            {
                this.ViewBag.MailSend = false;
            }

            return this.View();
        }

        public ActionResult ImageHelp()
        {
            return this.View();
        }

        [Route("sitemap.xml")]
        public ActionResult SitemapXml()
        {
            var nodes = new List<SitemapNode>();

            nodes.Add(new SitemapNode() { Priority = 1, Url = Url.RouteUrl("Default", new { action = "Index" }, this.Request.Url.Scheme) });
            nodes.Add(new SitemapNode() { Priority = 0.5, Url = Url.RouteUrl("Default", new { action = "Contacts" }, this.Request.Url.Scheme) });
            nodes.Add(new SitemapNode() { Priority = 0.6, Url = Url.RouteUrl("Default", new { controller = "Gallery", action = "Index" }, this.Request.Url.Scheme) });

            var products = this.products.Get().Where(p => p.Active).OrderByDescending(p => p.Id).ToList();
            foreach (var product in products)
            {
                nodes.Add(new SitemapNode()
                    {
                        Priority = 0.8,
                        Frequency = SitemapFrequency.Monthly,
                        Url = Url.RouteUrl("Product", new { acronym = product.Acronym }, this.Request.Url.Scheme)
                    });
            }

            var categories = this.categories.Get().OrderBy(c => c.Order).ToList();
            foreach (var category in categories)
            {
                nodes.Add(new SitemapNode()
                {
                    Priority = 0.6,
                    Frequency = SitemapFrequency.Weekly,
                    Url = Url.RouteUrl("GalleryCategory", new { acronym = category.Acronym }, this.Request.Url.Scheme)
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
