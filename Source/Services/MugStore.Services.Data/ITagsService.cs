namespace MugStore.Services.Data
{
    using System.Linq;
    using MugStore.Data.Models;

    public interface ITagsService
    {
        IQueryable<ProductTag> Get();

        ProductTag Get(int id);

        ProductTag Get(string acronym);

        void Create(ProductTag tag);
    }
}
