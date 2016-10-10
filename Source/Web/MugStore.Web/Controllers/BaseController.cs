namespace MugStore.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Common;
    using Infrastructure.Mapping;
    using Services.Data;

    public abstract class BaseController : Controller
    {
        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }

        protected void AddTagsToViewBag(ITagsService tags)
        {
            this.ViewBag.Tags = tags.Get().Where(t => t.Active).OrderBy(t => Guid.NewGuid()).Take(GlobalConstants.MaxTagsInFooter).ToList();
        }
    }
}
