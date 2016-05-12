namespace MugStore.Web.Controllers
{
    using System.IO;
    using System.Web.Mvc;
    using Common;
    using Services.Data;

    public class ImageController : BaseController
    {
        private readonly IImagesService images;

        public ImageController(IImagesService images)
        {
            this.images = images;
        }

        public ActionResult Index(string id)
        {
            var image = this.images.Get(id);
            if (image == null)
            {
                return this.HttpNotFound();
            }

            var type = image.ContentType.Split('/');
            var dir = this.Server.MapPath(GlobalConstants.PathToUploadImages + image.Path);
            var path = Path.Combine(dir, image.Name + "." + type[1]);
            return base.File(path, image.ContentType);
        }
    }
}
