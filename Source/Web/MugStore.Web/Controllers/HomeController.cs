namespace MugStore.Web.Controllers
{
    using System.Configuration;
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
            var products = this.products.Get().Where(c => c.Active).ToList();
            this.ViewBag.Cities = this.cities.Get().Where(c => c.Highlight).ToList();
            this.ViewBag.ShowRight = true;
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
    }
}
