namespace MugStore.Services.Data
{
    using MugStore.Data.Models;

    public interface IImagesService
    {
        void Add(Image image);

        Image Get(string name);
    }
}
