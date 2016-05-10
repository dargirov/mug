using MugStore.Data;
using MugStore.Data.Models;
using MugStore.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MugStore.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IOrdersService orders;

        public HomeController(IOrdersService orders)
        {
            this.orders = orders;
        }

        public ActionResult Index()
        {
            var order = new Order()
            {
                Quantity = 1
            };

            this.orders.Create(order);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}