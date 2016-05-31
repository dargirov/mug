namespace MugStore.Web.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Services.Data;
    using ViewModels.Gallery;

    public class GalleryController : BaseController
    {
        private readonly ICategoriesService categories;
        private readonly IProductsService products;
        private readonly ITagsService tags;

        public GalleryController(ICategoriesService categories, IProductsService products, ITagsService tags)
        {
            this.categories = categories;
            this.products = products;
            this.tags = tags;
        }

        public ActionResult Index()
        {
            var categories = this.categories.Get().OrderBy(c => c.Order).ToList();
            var products = this.products.Get().Where(p => p.Active).Include(p => p.Images).OrderByDescending(p => p.Id).ToList();
            this.AddTagsToViewBag(this.tags);

            var viewModel = new IndexViewModel()
            {
                Categories = categories,
                Products = products
            };

            return this.View(viewModel);
        }

        public ActionResult Category(string acronym)
        {
            var category = this.categories.Get(acronym);
            if (category == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var categories = this.categories.Get().OrderBy(c => c.Order).ToList();
            var products = this.products.Get().Where(p => p.Active && p.CategoryId == category.Id).Include(p => p.Images).OrderByDescending(p => p.Id).ToList();
            this.AddTagsToViewBag(this.tags);

            var viewModel = new IndexViewModel()
            {
                Categories = categories,
                Products = products
            };

            return this.View("Index", viewModel);
        }
    }
}
