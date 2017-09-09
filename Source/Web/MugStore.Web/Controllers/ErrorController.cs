namespace MugStore.Web.Controllers
{
    using System.Web.Mvc;
    using MugStore.Data.Models;
    using MugStore.Services.Common;
    using MugStore.Services.Data;

    public class ErrorController : BaseController
    {
        private readonly ITagsService tags;
        private readonly ILoggerService logger;

        public ErrorController(ITagsService tags, ILoggerService logger)
        {
            this.tags = tags;
            this.logger = logger;
        }

        public ActionResult Index()
        {
            this.AddTagsToViewBag(this.tags);
            this.logger.Log(LogLevel.Error, this.Request.QueryString["aspxerrorpath"], "500");

            this.Response.StatusCode = 500;
            return this.View();
        }

        public ActionResult NotFound()
        {
            this.AddTagsToViewBag(this.tags);
            this.logger.Log(LogLevel.Warn, this.Request.QueryString["aspxerrorpath"], "404");

            this.Response.StatusCode = 404;
            return this.View();
        }
    }
}