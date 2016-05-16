namespace MugStore.Services.Data
{
    using System.Linq;
    using MugStore.Data.Models;

    public interface ICitiesService
    {
        IQueryable<City> Get();
    }
}
