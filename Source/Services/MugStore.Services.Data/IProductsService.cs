namespace MugStore.Services.Data
{
    using System.Linq;
    using MugStore.Data.Models;

    public interface IProductsService
    {
        void Create(Product product);

        Product Get(string acronym);

        Product Get(int id);

        IQueryable<Product> Get();
    }
}
