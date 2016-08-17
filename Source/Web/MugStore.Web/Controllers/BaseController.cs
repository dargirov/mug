namespace MugStore.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
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
            this.ViewBag.Tags = tags.Get().Where(t => t.Active).OrderByDescending(t => t.Id).ToList();
        }
    }
}
