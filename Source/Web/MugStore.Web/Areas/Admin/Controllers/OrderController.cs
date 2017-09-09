namespace MugStore.Web.Areas.Admin.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using App_Start;
    using Data.Models;
    using Services.Data;
    using ViewModels.Order;
    using Web.Controllers;

    [AuthorizeUser]
    public class OrderController : BaseController
    {
        private readonly IOrdersService orders;
        private readonly IImagesService images;

        public OrderController(IOrdersService orders, IImagesService images)
        {
            this.orders = orders;
            this.images = images;
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
                throw new HttpException(404, id.ToString());
            }

            var viewModel = this.Mapper.Map<PreviewViewModel>(order);

            return this.View(viewModel);
        }

        public ActionResult UploadedImages()
        {
            var images = this.images.Get().OrderByDescending(i => i.Id).ToList();
            var viewModel = new UploadedImagesViewModel()
            {
                Images = images
            };

            return this.View(viewModel);
        }
    }
}
