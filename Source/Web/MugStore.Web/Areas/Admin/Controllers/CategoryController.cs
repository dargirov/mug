namespace MugStore.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using MugStore.Data.Models;
    using MugStore.Services.Data;
    using MugStore.Web.Areas.Admin.ViewModels.Category;
    using MugStore.Web.Controllers;
    using MugStore.Web.App_Start;

    [AuthorizeUser]
    public class CategoryController : BaseController
    {
        private readonly ICategoriesService categories;

        public CategoryController(ICategoriesService categories)
        {
            this.categories = categories;
        }

        public ActionResult Index()
        {
            var categories = this.categories.Get().OrderBy(c => c.Order).ToList();

            var viewModel = new IndexViewModel()
            {
                Categories = categories
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = this.categories.Get().Where(t => t.Name == model.Name || t.Acronym == model.Acronym).FirstOrDefault();
            if (category == null)
            {
                var newCategory = new Category()
                {
                    Name = model.Name,
                    Acronym = model.Acronym,
                    Order = model.Order,
                    Active = true
                };

                this.categories.Create(newCategory);
            }

            return this.RedirectToAction("Index", "Category");
        }
    }
}
