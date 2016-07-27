namespace MugStore.Services.Data
{
    using System.Linq;
    using MugStore.Data.Models;

    public interface IBulletinsService
    {
        IQueryable<Bulletin> Get();

        Bulletin Get(string email);

        void Add(Bulletin bulletin);
    }
}
