namespace MugStore.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using MugStore.Data.Models;
    using MugStore.Services.Data;
    using MugStore.Web.Areas.Admin.ViewModels.Order;
    using MugStore.Web.App_Start;
    using MugStore.Web.Controllers;

    [AuthorizeUser]
    public class OrderController : BaseController
    {
        private readonly IOrdersService orders;

        public OrderController(IOrdersService orders)
        {
            this.orders = orders;
        }

        public ActionResult Index()
        {
            var orders = this.orders.Get().Include(o => o.DeliveryInfo.City).OrderByDescending(o => o.Id).ToList();
            var viewModel = new IndexViewModel()
            {
                Orders = orders
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetStatus(int orderId, ConfirmationStatus confirmationStatus, OrderStatus orderStatus, string type)
        {
            var order = this.orders.Get(orderId);
            if (order == null)
            {
                return this.Json(new { success = false });
            }

            switch (type)
            {
                case "confirmation":
                    order.ConfirmationStatus = confirmationStatus;
                    break;
                case "order":
                    order.OrderStatus = orderStatus;
                    break;
            }

            this.orders.Save();

            return this.Json(new { success = true });
        }

        public ActionResult Preview(int id)
        {
            var order = this.orders.Get(id);
            if (order == null)
            {
                return this.HttpNotFound();
            }

            var viewModel = this.Mapper.Map<PreviewViewModel>(order);

            return this.View(viewModel);
        }
    }
}
