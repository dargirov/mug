namespace MugStore.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using MugStore.Services.Data;
    using MugStore.Web.ViewModels.Product;

    public class ProductController : BaseController
    {
        private readonly IProductsService products;
        private readonly ITagsService tags;

        public ProductController(IProductsService products, ITagsService tags)
        {
            this.products = products;
            this.tags = tags;
        }

        public ActionResult Index(string acronym)
        {
            var product = this.products.Get(acronym);
            if (product == null)
            {
                return this.HttpNotFound();
            }

            base.AddTagsToViewBag(tags);
            var viewModel = this.Mapper.Map<IndexViewModel>(product);

            return this.View(viewModel);
        }
    }
}
