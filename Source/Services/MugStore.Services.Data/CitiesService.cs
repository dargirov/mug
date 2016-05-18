namespace MugStore.Services.Data
{
    using System.Linq;
    using MugStore.Data.Common;
    using MugStore.Data.Models;

    public class CitiesService : ICitiesService
    {
        private readonly IDbRepository<City> cities;

        public CitiesService(IDbRepository<City> cities)
        {
            this.cities = cities;
        }

        public IQueryable<City> Get()
        {
            return this.cities.All().OrderByDescending(c => c.Highlight).ThenBy(c => c.PostCode);
        }


        public City Get(int id)
        {
            return this.cities.GetById(id);
        }
    }
}
