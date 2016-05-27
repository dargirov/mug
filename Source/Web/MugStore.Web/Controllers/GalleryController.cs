﻿namespace MugStore.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Data.Entity; 
    using MugStore.Services.Data;
    using MugStore.Web.ViewModels.Gallery;

    public class GalleryController : BaseController
    {
        private readonly ICategoriesService categories;
        private readonly IProductsService products;

        public GalleryController(ICategoriesService categories, IProductsService products)
        {
            this.categories = categories;
            this.products = products;
        }

        public ActionResult Index()
        {
            var categories = this.categories.Get().OrderBy(c => c.Order).ToList();
            var products = this.products.Get().Include(p => p.Images).OrderByDescending(p => p.Id).ToList();

            var viewModel = new IndexViewModel()
            {
                Categories = categories,
                Products = products
            };

            return this.View(viewModel);
        }
    }
}
