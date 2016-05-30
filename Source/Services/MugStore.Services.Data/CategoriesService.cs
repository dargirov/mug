namespace MugStore.Services.Data
{
    using System.Linq;
    using MugStore.Data.Common;
    using MugStore.Data.Models;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDbRepository<Category> categories;

        public CategoriesService(IDbRepository<Category> categories)
        {
            this.categories = categories;
        }

        public IQueryable<Category> Get()
        {
            return this.categories.All().Where(c => c.Active);
        }


        public void Create(Category category)
        {
            this.categories.Add(category);
            this.categories.Save();
        }


        public Category Get(string acronym)
        {
            return this.categories.All().Where(c => c.Acronym == acronym).FirstOrDefault();
        }
    }
}
