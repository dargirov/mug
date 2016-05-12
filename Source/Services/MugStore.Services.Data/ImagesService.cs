namespace MugStore.Services.Data
{
    using MugStore.Data.Common;
    using MugStore.Data.Models;

    public class ImagesService : IImagesService
    {
        private readonly IDbRepository<Image> images;

        public ImagesService(IDbRepository<Image> images)
        {
            this.images = images;
        }

        public void Add(Image image)
        {
            this.images.Add(image);
            this.images.Save();
        }
    }
}
