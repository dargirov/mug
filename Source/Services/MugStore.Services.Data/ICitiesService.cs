namespace MugStore.Services.Data
{
    using System.Linq;
    using MugStore.Data.Models;

    public interface ICitiesService
    {
        IQueryable<City> Get();

        City Get(int id);

        void Create(City city);

        void Save();
    }
}
