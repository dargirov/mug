﻿namespace MugStore.Web.Controllers
{
    using System;
    using System.Configuration;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using Services.Data;
    using ViewModels.Order;

    public class OrderController : Controller
    {
        private readonly IOrdersService orders;
        private readonly IImagesService images;
        private readonly ICitiesService cities;
        private readonly IProductsService products;
        private readonly ICouriersService couriers;

        public OrderController(IOrdersService orders, IImagesService images, ICitiesService cities, IProductsService products, ICouriersService couriers)
        {
            this.orders = orders;
            this.images = images;
            this.cities = cities;
            this.products = products;
            this.couriers = couriers;
        }

        public ActionResult Create(CreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json(new { status = "error" }, JsonRequestBehavior.AllowGet);
            }

            var city = this.cities.Get(model.DeliveryInfo.CityId);
            if (city == null)
            {
                return this.Json(new { status = "error", message = "Invalid city id" }, JsonRequestBehavior.AllowGet);
            }

            Courier courier = null;
            if (model.DeliveryInfo.CourierId.HasValue)
            {
                courier = this.couriers.Get(model.DeliveryInfo.CourierId.Value);
                if (courier == null)
                {
                    return this.Json(new { status = "error", message = "Invalid courier id" }, JsonRequestBehavior.AllowGet);
                }
            }

            var order = new Order()
            {
                Acronym = this.GenerateAcronym(),
                Quantity = model.Quantity,
                PriceCustomer = decimal.Parse(ConfigurationManager.AppSettings["SingleMugPrice"]),
                PriceSupplier = decimal.Parse(ConfigurationManager.AppSettings["SungleMugPriceSupplier"]),
                PriceDelivery = decimal.Parse(ConfigurationManager.AppSettings["DeliveryPrice"]),
                PaymentMethod = model.PaymentMethod,
                DeliveryInfo = new DeliveryInfo()
                {
                    FullName = model.DeliveryInfo.FullName,
                    Address = model.DeliveryInfo.Address,
                    CityId = model.DeliveryInfo.CityId,
                    Comment = model.DeliveryInfo.Comment,
                    Phone = model.DeliveryInfo.Phone,
                    CourierId = model.DeliveryInfo.CourierId
                },
                ConfirmationStatus = ConfirmationStatus.Pending,
                OrderStatus = OrderStatus.InProgress
            };

            if (model.ProductAcronym != null)
            {
                var product = this.products.Get(model.ProductAcronym);
                if (product == null)
                {
                    throw new HttpException(404, model.ProductAcronym);
                }

                order.Product = product;
            }

            this.orders.Create(order);

            if (model.ProductAcronym == null)
            {
                foreach (var imageInfo in model.Images)
                {
                    var image = this.images.Get(imageInfo.Name);
                    image.OrderId = order.Id;
                    image.Rotation = imageInfo.Rotation;
                    image.Y = imageInfo.Y;
                }

                this.images.Save();
            }

            var result = new
            {
                status = "success",
                acronym = order.Acronym,
                paymentMethod = order.PaymentMethod,
                fullName = order.DeliveryInfo.FullName,
                address = order.DeliveryInfo.Address,
                phone = order.DeliveryInfo.Phone,
                comment = order.DeliveryInfo.Comment,
                city = city.Name,
                courier = courier?.Name,
                quantity = order.Quantity,
                price = (order.Quantity * decimal.Parse(ConfigurationManager.AppSettings["SingleMugPrice"])) +decimal.Parse(ConfigurationManager.AppSettings["DeliveryPrice"])
            };

            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        private string GenerateAcronym()
        {
            var date = DateTime.UtcNow;
            var rand = new Random();

            var acronym = string.Format("{0}{1}", date.ToString("yyMMdd"), rand.Next(1000, 9999));
            var order = this.orders.Get(acronym);

            while (order != null)
            {
                acronym = string.Format("{0}{1}", date.ToString("yyMMdd"), rand.Next(1000, 9999));
                order = this.orders.Get(acronym);
            }

            return acronym;
        }
    }
}
