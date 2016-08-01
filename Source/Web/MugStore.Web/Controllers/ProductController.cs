namespace MugStore.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Services.Data;
    using ViewModels.Product;

    public class ProductController : BaseController
    {
        private readonly IProductsService products;
        private readonly ITagsService tags;
        private readonly ICitiesService cities;

        public ProductController(IProductsService products, ITagsService tags, ICitiesService cities)
        {
            this.products = products;
            this.tags = tags;
            this.cities = cities;
        }

        public ActionResult Index(string acronym)
        {
            var product = this.products.Get(acronym);
            if (product == null)
            {
                return this.HttpNotFound();
            }

            this.ViewBag.Cities = this.cities.Get().Where(c => c.Highlight).ToList();
            this.ViewBag.ShowRight = false;
            this.ViewBag.PageHeading = product.Title;
            this.AddTagsToViewBag(this.tags);
            var viewModel = this.Mapper.Map<IndexViewModel>(product);

            return this.View(viewModel);
        }
    }
}
