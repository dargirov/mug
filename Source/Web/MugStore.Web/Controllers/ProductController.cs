﻿namespace MugStore.Web.Controllers
{
    using System.Configuration;
    using System.Linq;
    using System.Web.Mvc;
    using Common;
    using Services.Data;
    using ViewModels.Product;

    public class ProductController : BaseController
    {
        private readonly IProductsService products;
        private readonly ITagsService tags;
        private readonly ICitiesService cities;
        private readonly ICouriersService couriers;

        public ProductController(IProductsService products, ITagsService tags, ICitiesService cities, ICouriersService couriers)
        {
            this.products = products;
            this.tags = tags;
            this.cities = cities;
            this.couriers = couriers;
        }

        public ActionResult Index(string acronym)
        {
            var product = this.products.Get(acronym);
            if (product == null)
            {
                return this.HttpNotFound();
            }

            this.ViewBag.Cities = this.cities.Get().Where(c => c.Highlight).OrderBy(x => x.Name).ToList();
            this.ViewBag.Couriers = this.couriers.Get().Where(c => c.Active).OrderBy(x => x.Id).ToList();
            this.ViewBag.ShowRight = false;
            this.ViewBag.PageHeading = product.Title;
            this.ViewBag.SingleMugPrice = decimal.Parse(ConfigurationManager.AppSettings["SingleMugPrice"]);
            this.ViewBag.DeliveryPrice = decimal.Parse(ConfigurationManager.AppSettings["DeliveryPrice"]);
            this.AddTagsToViewBag(this.tags);
            var viewModel = this.Mapper.Map<IndexViewModel>(product);

            return this.View(viewModel);
        }
    }
}
