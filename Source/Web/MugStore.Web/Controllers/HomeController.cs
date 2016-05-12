namespace MugStore.Web.Controllers
{
    using System;
    using System.IO;
    using System.Web.Mvc;
    using Common;
    using Data.Models;
    using Services.Data;

    public class HomeController : BaseController
    {
        private readonly IOrdersService orders;
        private readonly IImagesService images;

        public HomeController(IOrdersService orders, IImagesService images)
        {
            this.orders = orders;
            this.images = images;
        }

        public ActionResult Index()
        {
            //var order = new Order()
            //{
            //    Quantity = 1
            //};

            //this.orders.Add(order);

            return View();
        }

        public ActionResult About()
        {
            this.ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            this.ViewBag.Message = "Your contact page.";


            return View();
        }

        public ActionResult Order()
        {

            return null;
        }

        public ActionResult Upload()
        {
            var rootPath = this.Server.MapPath(GlobalConstants.PathToUploadImages);
            var datePath = string.Format(@"{0}\{1}\", DateTime.Today.Year, DateTime.Today.Month);
            var path = rootPath + datePath;
            

            var file = this.Request.Files[0];
            var type = file.ContentType.Split('/');
            var fileName = Guid.NewGuid().ToString();

            Stream stream = file.InputStream;
            System.Drawing.Image imageData;
            try
            {
                imageData = System.Drawing.Image.FromStream(stream);
            }
            catch (ArgumentException ex)
            {
                return this.Json(new 
                { 
                    status = "error",
                    message = ex.Message
                });
            }
            
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            file.SaveAs(path + fileName + "." + type[1]);

            var order = this.GetOrderFromSession();
            if (order == null)
            {
                order = this.CreateOrder();
                this.Session["orderId"] = order.Id;
            }

            var image = new Image()
            {
                Name = fileName,
                OriginalName = file.FileName,
                ContentType = file.ContentType,
                Path = datePath,
                Width = imageData.Width,
                Height = imageData.Height,
                Dpi = (int)Math.Ceiling(imageData.HorizontalResolution)
            };

            order.Images.Add(image);
            this.images.Add(image);

            var result = new
            {
                status = "ok",
                filename = fileName,
                width = imageData.Width,
                height = imageData.Height,
                dpi = (int)Math.Ceiling(imageData.HorizontalResolution)
            };

            return this.Json(result);
        }

        private Order CreateOrder()
        {
            var order = new Order();
            this.orders.Create(order);
            return order;
        }

        private Order GetOrderFromSession()
        {
            var id = (int?)this.Session["orderId"];
            if (id == null)
            {
                return null;
            }

            int idAsInt = int.Parse(id.ToString());
            return this.orders.Get(idAsInt);
        }
    }
}