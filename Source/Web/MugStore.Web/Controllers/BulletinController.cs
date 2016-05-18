namespace MugStore.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using MugStore.Services.Data;
    using MugStore.Web.ViewModels.Bulletin;
    using MugStore.Data.Models;

    public class BulletinController : BaseController
    {
        private readonly IBulletinsService bulletins;

        public BulletinController(IBulletinsService bulletins)
        {
            this.bulletins = bulletins;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(BulletinInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(new { success = false, message = "Invalid email" }, JsonRequestBehavior.AllowGet);
            }

            var bulletin = this.bulletins.Get(model.Email);
            if (bulletin != null)
            {
                return this.Json(new { success = false, message = "Email exists" }, JsonRequestBehavior.AllowGet);
            }

            this.bulletins.Add(new Bulletin()
            {
                Email = model.Email
            });

            return this.Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}
