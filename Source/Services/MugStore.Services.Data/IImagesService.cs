namespace MugStore.Services.Data
{
    using System.Linq;
    using MugStore.Data.Models;

    public interface IImagesService
    {
        void Add(Image image);

        IQueryable<Image> Get();

        Image Get(string name);

        ProductImage GetProductImage(string name);

        void Save();
    }
}
