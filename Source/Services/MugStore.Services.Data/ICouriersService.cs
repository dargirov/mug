namespace MugStore.Services.Data
{
    using System.Linq;
    using MugStore.Data.Models;

    public interface ICouriersService
    {
        IQueryable<Courier> Get();

        Courier Get(int id);
    }
}
