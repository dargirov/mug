namespace MugStore.Services.Data
{
    using System.Linq;
    using MugStore.Data.Common;
    using MugStore.Data.Models;

    public class ProductsService : IProductsService
    {
        private readonly IDbRepository<Product> products;

        public ProductsService(IDbRepository<Product> products)
        {
            this.products = products;
        }

        public void Create(Product product)
        {
            this.products.Add(product);
            this.products.Save();
        }

        public Product Get(string acronym)
        {
            return this.products.All().Where(p => p.Acronym == acronym).FirstOrDefault();
        }

        public IQueryable<Product> Get()
        {
            return this.products.All();
        }

        public Product Get(int id)
        {
            return this.products.GetById(id);
        }

        public void Save()
        {
            this.products.Save();
        }
    }
}
