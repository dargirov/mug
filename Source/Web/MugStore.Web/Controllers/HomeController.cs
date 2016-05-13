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

        

        //private Order CreateOrder()
        //{
        //    var order = new Order();
        //    this.orders.Create(order);
        //    return order;
        //}

        //private Order GetOrderFromSession()
        //{
        //    var id = (int?)this.Session["orderId"];
        //    if (id == null)
        //    {
        //        return null;
        //    }

        //    int idAsInt = int.Parse(id.ToString());
        //    return this.orders.Get(idAsInt);
        //}
    }
}