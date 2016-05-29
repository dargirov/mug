namespace MugStore.Web.Areas.Admin.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Data.Models;
    using Services.Data;
    using ViewModels.Tag;
    using Web.Controllers;

    public class TagController : BaseController
    {
        private readonly ITagsService tags;

        public TagController(ITagsService tags)
        {
            this.tags = tags;
        }

        public ActionResult Index()
        {
            var tags = this.tags.Get().Include(t => t.Products).ToList();

            var viewModel = new IndexViewModel()
            {
                Tags = tags
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

            var tag = this.tags.Get().Where(t => t.Name == model.Name || t.Acronym == model.Acronym).FirstOrDefault();
            if (tag == null)
            {
                var newTag = new ProductTag()
                {
                    Name = model.Name,
                    Acronym = model.Acronym,
                    Active = true
                };

                this.tags.Create(newTag);
            }

            return this.RedirectToAction("Index", "Tag");
        }
    }
}
