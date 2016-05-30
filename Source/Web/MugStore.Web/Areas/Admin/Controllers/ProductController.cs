namespace MugStore.Web.Areas.Admin.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Common;
    using Data.Models;
    using Services.Data;
    using ViewModels.Product;
    using Web.Controllers;
    using MugStore.Web.App_Start;

    [AuthorizeUser]
    public class ProductController : BaseController
    {
        private readonly ICategoriesService categories;
        private readonly IProductsService products;
        private readonly ITagsService tags;

        public ProductController(ICategoriesService categories, IProductsService products, ITagsService tags)
        {
            this.categories = categories;
            this.products = products;
            this.tags = tags;
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
            viewModel.AllTags = this.tags.Get().Where(t => t.Active).ToList();

            return this.View(viewModel);
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

            return this.RedirectToAction("Edit", "Product", new { id = id });
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

            return this.RedirectToAction("Edit", "Product", new { area = "Admin", id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTag(int tagId, int productId, string type)
        {
            var product = this.products.Get(productId);
            var tag = this.tags.Get(tagId);

            if (product == null || tag == null)
            {
                return this.Json(new { success = false });
            }

            switch (type)
            {
                case "add":
                    product.Tags.Add(tag);
                    break;
                case "remove":
                    product.Tags.Remove(tag);
                    break;
            }

            this.products.Save();

            return this.Json(new { success = true });
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
