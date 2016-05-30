namespace MugStore.Services.Data
{
    using System.Linq;
    using MugStore.Data.Models;

    public interface ICategoriesService
    {
        IQueryable<Category> Get();

        void Create(Category category);
    }
}
