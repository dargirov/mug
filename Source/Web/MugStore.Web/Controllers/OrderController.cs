using MugStore.Web.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MugStore.Web.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Create(CreateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(new { }, JsonRequestBehavior.AllowGet);
            }


            return this.Json(new { }, JsonRequestBehavior.AllowGet);
        }
    }
}