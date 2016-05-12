﻿using MugStore.Data;
using MugStore.Data.Models;
using MugStore.Services.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MugStore.Common;

namespace MugStore.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IOrdersService orders;
        private readonly IImagesService images;

        public HomeController(IOrdersService orders, IImagesService images)
        {
            this.orders = orders;
            this.images = images;
        }

        public ActionResult Index()
        {
            //var order = new Order()
            //{
            //    Quantity = 1
            //};

            //this.orders.Add(order);

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

        public ActionResult Upload()
        {
            var rootPath = Server.MapPath(GlobalConstants.PathToUploadImages);
            var datePath = string.Format(@"{0}\{1}\", DateTime.Today.Year, DateTime.Today.Month);
            var path = rootPath + datePath;

            var file = this.Request.Files[0];
            Stream stream = file.InputStream;
            System.Drawing.Image imageData = System.Drawing.Image.FromStream(stream);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            file.SaveAs(path + file.FileName);

            var order = this.GetOrderFromSession();
            if (order == null)
            {
                order = this.CreateOrder();
            }

            var image = new Image()
            {
                Name = file.FileName,
                Path = datePath,
                Width = imageData.Width,
                Height = imageData.Height,
                Dpi = (int)Math.Ceiling(imageData.HorizontalResolution)
            };

            order.Images.Add(image);
            this.images.Add(image);         

            var result = new
            {
                filename = "zack.jpg",
                width = imageData.Width,
                height = imageData.Height,
                dpi = (int)Math.Ceiling(imageData.HorizontalResolution)
            };

            return this.Json(result);
        }

        private Order CreateOrder()
        {
            var order = new Order();
            this.orders.Create(order);
            return order;
        }

        private Order GetOrderFromSession()
        {
            var id = (int?)this.Session["orderId"];
            if (id == null)
            {
                return null;
            }

            int idAsInt = int.Parse(id.ToString());
            return this.orders.Get(idAsInt);
        }
    }
}