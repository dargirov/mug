namespace MugStore.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Web.Mvc;
    using Services.Data;
    using ViewModels.Home;

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
            var cities = this.cities.Get().ToList();
            var products = this.products.Get().Where(c => c.Active).ToList();
            base.AddTagsToViewBag(this.tags);

            var viewModel = new IndexViewModel()
            {
                Cities = cities,
                Products = products
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult Contacts()
        {
            base.AddTagsToViewBag(this.tags);
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contacts(ContactsInputModel model)
        {
            base.AddTagsToViewBag(this.tags);

            if (this.ModelState.IsValid)
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress("shinobi.away@gmail.com"));
                message.From = new MailAddress("shinobi@mail.bg");
                message.Subject = "Test mail";
                message.Body = "emi de da znam";
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "argirov@outlook.com",  // replace with valid value
                        Password = "ZZZZZZZZZZ"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    //smtp.Send(message);
                    this.ViewBag.MailSend = true;
                }
            }
            else
            {
                this.ViewBag.MailSend = false;
            }

            return this.View();
        }

        public ActionResult Tag(string acronym)
        {
            var tag = this.tags.Get(acronym);
            if (tag == null)
            {
                return this.HttpNotFound();
            }

            base.AddTagsToViewBag(this.tags);
            var products = tag.Products.Where(p => p.Active).ToList();
            var categories = this.categories.Get().OrderBy(c => c.Order).ToList();

            var viewModel = new ViewModels.Gallery.IndexViewModel()
            {
                Products = products,
                Categories = categories
            };

            return this.View("~/Views/Gallery/Index.cshtml", viewModel);
        }
    }
}
