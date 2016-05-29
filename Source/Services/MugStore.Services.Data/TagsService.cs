namespace MugStore.Services.Data
{
    using System.Linq;
    using MugStore.Data.Common;
    using MugStore.Data.Models;

    public class TagsService : ITagsService
    {
        private readonly IDbRepository<ProductTag> tags;

        public TagsService(IDbRepository<ProductTag> tags)
        {
            this.tags = tags;
        }

        public void Create(ProductTag tag)
        {
            this.tags.Add(tag);
            this.tags.Save();
        }

        public IQueryable<ProductTag> Get()
        {
            return this.tags.All();
        }

        public ProductTag Get(int id)
        {
            return this.tags.GetById(id);
        }

        public ProductTag Get(string name)
        {
            return this.tags.All().Where(t => t.Name == name).FirstOrDefault();
        }
    }
}
