namespace MugStore.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using MugStore.Data.Models;
    using MugStore.Services.Data;
    using MugStore.Web.App_Start;
    using MugStore.Web.Areas.Admin.ViewModels.Blog;
    using MugStore.Web.Controllers;

    [AuthorizeUser]
    public class BlogController : BaseController
    {
        private readonly IBlogService blog;
        private readonly ITagsService tags;

        public BlogController(IBlogService blog, ITagsService tags)
        {
            this.blog = blog;
            this.tags = tags;
        }

        public ActionResult Index()
        {
            var posts = this.blog.GetPosts(x => true).ToList();

            var viewModel = new IndexViewModel()
            {
                Posts = posts
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new CreateViewModel();

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

            var post = new Post()
            {
                Title = model.Title,
                PageTitle = model.PageTitle,
                Acronym = model.Acronym,
                Active = model.Active,
                ShortDescription = model.ShortDescription,
                FullDescription = model.FullDescription,
                PageDescription = model.PageDescription,
            };

            this.blog.Create(post);

            return this.RedirectToAction("Edit", "Blog", new { id = post.Id });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var post = this.blog.GetPost(id);
            if (post == null)
            {
                throw new HttpException(404, id.ToString());
            }

            var viewModel = this.Mapper.Map<CreateViewModel>(post);
            viewModel.AllTags = this.tags.GetPostTag().Where(t => t.Active).ToList();

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CreateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Edit", "Blog", new { id = id });
            }

            var post = this.blog.GetPost(id);
            if (post == null)
            {
                throw new HttpException(404, id.ToString());
            }

            post.Title = model.Title;
            post.PageTitle = model.PageTitle;
            post.Acronym = model.Acronym;
            post.Active = model.Active;
            post.ShortDescription = model.ShortDescription;
            post.FullDescription = model.FullDescription;
            post.PageDescription = model.PageDescription;
            this.blog.Save();

            return this.RedirectToAction("Edit", "Blog", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTag(int tagId, int postId, string type)
        {
            var post = this.blog.GetPost(postId);
            var tag = this.tags.GetPostTag(tagId);

            if (post == null || tag == null)
            {
                return this.Json(new { success = false });
            }

            switch (type)
            {
                case "add":
                    post.Tags.Add(tag);
                    break;
                case "remove":
                    post.Tags.Remove(tag);
                    break;
            }

            this.blog.Save();

            return this.Json(new { success = true });
        }
    }
}