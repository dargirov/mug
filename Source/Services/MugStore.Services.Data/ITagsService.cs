namespace MugStore.Services.Data
{
    using System.Linq;
    using MugStore.Data.Models;

    public interface ITagsService
    {
        IQueryable<ProductTag> GetProductTag();

        IQueryable<PostTag> GetPostTag();

        ProductTag GetProductTag(int id);

        PostTag GetPostTag(int id);

        ProductTag GetProductTag(string acronym);

        PostTag GetPostTag(string acronym);

        void Create(ProductTag tag);

        void Create(PostTag tag);
    }
}
