namespace MugStore.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using MugStore.Services.Data;
    using MugStore.Web.ViewModels.Blog;

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
            this.ViewBag.PageDescription = "Блог";
            this.AddTagsToViewBag(this.tags);

            var viewModel = new IndexViewModel()
            {
                Posts = this.blog.GetPosts().ToList()
            };

            return this.View(viewModel);
        }

        public ActionResult Post(string acronym)
        {
            if (string.IsNullOrWhiteSpace(acronym))
            {
                return this.HttpNotFound();
            }

            var post = this.blog.GetPost(acronym);
            if (post == null)
            {
                return this.HttpNotFound();
            }

            this.ViewBag.PageDescription = post.PageDescription;
            this.AddTagsToViewBag(this.tags);
            var viewModel = this.Mapper.Map<PostViewModel>(post);

            return this.View(viewModel);
        }
    }
}