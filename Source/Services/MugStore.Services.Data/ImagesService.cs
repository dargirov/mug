namespace MugStore.Services.Data
{
    using System.Linq;
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


        public Image Get(string name)
        {
            return this.images.All().Where(i => i.Name == name).FirstOrDefault();
        }


        public void Save()
        {
            this.images.Save();
        }
    }
}
