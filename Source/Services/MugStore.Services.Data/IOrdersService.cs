namespace MugStore.Services.Data
{
    using System.Linq;
    using MugStore.Data.Models;

    public interface IOrdersService
    {
        void Create(Order order);

        IQueryable<Order> Get();

        Order Get(int id);

        Order Get(string acronym);

        void Save();
    }
}
