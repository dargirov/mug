using MugStore.Common;
using MugStore.Services.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MugStore.Web.Controllers
{
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
                return null;
            }

            var type = image.ContentType.Split('/');
            var dir = Server.MapPath(GlobalConstants.PathToUploadImages + image.Path);
            var path = Path.Combine(dir, image.Name + "." + type[1]);
            return base.File(path, image.ContentType);
        }
    }
}