namespace MugStore.Services.Data
{
    using System.Linq;
    using MugStore.Data.Common;
    using MugStore.Data.Models;

    public class CouriersService : ICouriersService
    {
        private readonly IDbRepository<Courier> couriers;

        public CouriersService(IDbRepository<Courier> couriers)
        {
            this.couriers = couriers;
        }

        public IQueryable<Courier> Get()
        {
            return this.couriers.All();
        }

        public Courier Get(int id)
        {
            return this.couriers.GetById(id);
        }
    }
}
