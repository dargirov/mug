using MugStore.Data;
using MugStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MugStore.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new ApplicationDbContext())
            {
                db.Images.Add(new Image() { Name = "sadfasfadsf" });
                db.SaveChanges();

                foreach (var i in db.Images)
                {
                    this.Response.AddHeader(i.Name, i.Name);
                }
            }
            //var city = new City();
            //city.Name = "Asgr";
            //var c = new ApplicationDbContext();
            //c.Cities.Add(city);
            //c.SaveChanges();
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