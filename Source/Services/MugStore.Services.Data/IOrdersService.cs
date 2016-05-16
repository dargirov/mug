namespace MugStore.Services.Data
{
    using MugStore.Data.Models;

    public interface IOrdersService
    {
        void Create(Order order);

        Order Get(int id);

        Order Get(string acronym);
    }
}
