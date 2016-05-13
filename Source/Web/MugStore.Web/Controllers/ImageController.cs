namespace MugStore.Web.Controllers
{
    using System;
    using System.IO;
    using System.Web.Mvc;
    using Common;
    using Data.Models;
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
            return this.File(path, image.ContentType);
        }

        public ActionResult Upload()
        {
            try
            {
                var imagesPath = this.Server.MapPath(GlobalConstants.PathToUploadImages);
                var datePath = string.Format(@"{0}\{1}\", DateTime.Today.Year, DateTime.Today.Month);
                var path = imagesPath + datePath;

                if (this.Request.Files.Count != 1)
                {
                    throw new InvalidDataException("Upload 1 file");
                }

                var file = this.Request.Files[0];
                var type = file.ContentType.Split('/');
                var fileName = Guid.NewGuid().ToString();

                var stream = file.InputStream;
                var imageData = System.Drawing.Image.FromStream(stream);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                file.SaveAs(path + fileName + "." + type[1]);

                var image = new Image()
                {
                    Name = fileName,
                    OriginalName = file.FileName,
                    ContentType = file.ContentType,
                    Path = datePath,
                    Width = imageData.Width,
                    Height = imageData.Height,
                    Dpi = (int)Math.Ceiling(imageData.HorizontalResolution)
                };

                this.images.Add(image);

                var result = new
                {
                    status = "ok",
                    filename = image.Name,
                    width = image.Width,
                    height = image.Height,
                    dpi = image.Dpi
                };

                return this.Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    status = "error",
                    message = ex.Message
                };

                return this.Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
