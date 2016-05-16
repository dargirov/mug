namespace MugStore.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Services.Data;
    using MugStore.Data.Models;
    using System.Collections.Generic;

    public class HomeController : BaseController
    {
        private readonly ICitiesService cities;

        public HomeController(ICitiesService cities)
        {
            this.cities = cities;
        }

        public ActionResult Index()
        {
            var cities = this.cities.Get().ToList();
            ViewBag.Cities = cities;

            return View();
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