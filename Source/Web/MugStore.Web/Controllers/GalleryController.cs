namespace MugStore.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using MugStore.Services.Data;
    using MugStore.Web.ViewModels.Gallery;

    public class GalleryController : BaseController
    {
        private readonly ICategoriesService categories;

        public GalleryController(ICategoriesService categories)
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
    }
}
