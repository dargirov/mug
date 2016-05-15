namespace MugStore.Web.Controllers
{
    using System.Web.Mvc;
    using ViewModels.Order;

    public class OrderController : Controller
    {
        public ActionResult Create(CreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(new { }, JsonRequestBehavior.AllowGet);
            }

            return this.Json(new { }, JsonRequestBehavior.AllowGet);
        }
    }
}