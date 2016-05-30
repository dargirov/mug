namespace MugStore.Services.Data
{
    using System.Linq;
    using MugStore.Data.Common;
    using MugStore.Data.Models;

    public class OrdersService : IOrdersService
    {
        private readonly IDbRepository<Order> orders;

        public OrdersService(IDbRepository<Order> orders)
        {
            this.orders = orders;
        }

        public void Create(Order order)
        {
            this.orders.Add(order);
            this.orders.Save();
        }

        public Order Get(int id)
        {
            return this.orders.GetById(id);
        }

        public Order Get(string acronym)
        {
            return this.orders.All().Where(o => o.Acronym == acronym).FirstOrDefault();
        }

        public IQueryable<Order> Get()
        {
            return this.orders.All();
        }

        public void Save()
        {
            this.orders.Save();
        }
    }
}
