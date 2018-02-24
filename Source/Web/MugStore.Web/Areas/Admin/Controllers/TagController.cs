namespace MugStore.Web.Areas.Admin.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using App_Start;
    using Data.Models;
    using Services.Data;
    using ViewModels.Tag;
    using Web.Controllers;

    [AuthorizeUser]
    public class TagController : BaseController
    {
        private readonly ITagsService tags;

        public TagController(ITagsService tags)
        {
            this.tags = tags;
        }

        public ActionResult Index()
        {
            var productTags = this.tags.GetProductTag().Include(t => t.Products).ToList();
            var postTags = this.tags.GetPostTag().Include(t => t.Posts).ToList();

            var viewModel = new IndexViewModel()
            {
                ProductTags = productTags,
                PostTags = postTags
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

            var tag = model.IsProductTag
                ? this.tags.GetProductTag().Any(t => t.Name == model.Name || t.Acronym == model.Acronym)
                : this.tags.GetPostTag().Any(t => t.Name == model.Name || t.Acronym == model.Acronym);

            if (!tag)
            {
                if (model.IsProductTag)
                {
                    var newTag = new ProductTag() { Name = model.Name, Acronym = model.Acronym, Active = true };
                    this.tags.Create(newTag);
                }
                else
                {
                    var newTag = new PostTag() { Name = model.Name, Acronym = model.Acronym, Active = true };
                    this.tags.Create(newTag);
                }
            }

            return this.RedirectToAction("Index", "Tag");
        }
    }
}
