namespace MugStore.Services.Data
{
    using MugStore.Data.Models;

    public interface IBulletinsService
    {
        Bulletin Get(string email);

        void Add(Bulletin bulletin);
    }
}
