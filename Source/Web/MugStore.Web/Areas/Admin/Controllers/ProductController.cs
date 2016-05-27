namespace MugStore.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using MugStore.Web.Controllers;
    using MugStore.Services.Data;
    using MugStore.Web.Areas.Admin.ViewModels.Product;
    using MugStore.Data.Models;
    using MugStore.Common;
    using System.IO;

    public class ProductController : BaseController
    {
        private readonly ICategoriesService categories;
        private readonly IProductsService products;

        public ProductController(ICategoriesService categories, IProductsService products)
        {
            this.categories = categories;
            this.products = products;
        }

        public ActionResult Index()
        {
            var products = this.products.Get().OrderByDescending(p => p.Id).ToList();
            var model = new IndexViewModel()
            {
                Products = products
            };

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = this.products.Get(id);
            if (product == null)
            {
                return this.HttpNotFound();
            }

            var viewModel = this.Mapper.Map<CreateViewModel>(product);
            viewModel.Categories = this.categories.Get();

            return this.View("Create", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CreateViewModel model)
        {
            model.Categories = this.categories.Get();

            if (!this.ModelState.IsValid)
            {
                return this.View("Create", model);
            }

            var product = this.products.Get(id);
            if (product == null)
            {
                return this.HttpNotFound();
            }

            product.Title = model.Title;
            product.Description = model.Description;
            product.Code = model.Code;
            product.PreviewData = model.PreviewData;
            product.Active = model.Active;
            product.CategoryId = model.CategoryId;
            this.products.Save();

            return RedirectToAction("Edit", "Product", new { id = id });
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new CreateViewModel()
            {
                Categories = this.categories.Get()
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var product = new Product()
            {
                Active = model.Active,
                CategoryId = model.CategoryId,
                Code = model.Code,
                Description = model.Description,
                Title = model.Title,
                PreviewData = model.PreviewData,
                Acronym = this.GenerateAcronym()
            };

            this.products.Create(product);
            model.Categories = this.categories.Get();

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(int id, HttpPostedFileBase file)
        {
            var product = this.products.Get(id);
            if (product == null)
            {
                return this.HttpNotFound();
            }

            if (file.ContentLength > 0)
            {
                var imagesPath = this.Server.MapPath(GlobalConstants.PathToGalleryImages);
                var path = imagesPath + id.ToString();

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var name = Guid.NewGuid().ToString();
                var type = file.ContentType.Split('/');
                file.SaveAs(path + @"\" + name + "." + type[1]);
                var image = new ProductImage()
                {
                    Name = name,
                    OriginalName = file.FileName,
                    Path = id.ToString(),
                    ContentType = file.ContentType,
                    ProductId = id
                };

                product.Images.Add(image);
                this.products.Save();
            }

            return RedirectToAction("Edit", "Product", new { area = "Admin", id = id });
        }

        private string GenerateAcronym()
        {
            var date = DateTime.UtcNow;
            var rand = new Random();

            var acronym = string.Format("{0}{1}", date.ToString("yyMMdd"), rand.Next(1000, 9999));
            var product = this.products.Get(acronym);

            while (product != null)
            {
                acronym = string.Format("{0}{1}", date.ToString("yyMMdd"), rand.Next(1000, 9999));
                product = this.products.Get(acronym);
            }

            return acronym;
        }
    }
}
