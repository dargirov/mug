namespace MugStore.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using MugStore.Web.Controllers;

    public class ProductController : BaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            return null;
        }
    }
}
