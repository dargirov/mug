namespace MugStore.Services.Data
{
    using System.Linq;
    using MugStore.Data.Common;
    using MugStore.Data.Models;

    public class BulletinsService : IBulletinsService
    {
        private readonly IDbRepository<Bulletin> bulletin;

        public BulletinsService(IDbRepository<Bulletin> bulletin)
        {
            this.bulletin = bulletin;
        }

        public void Add(Bulletin bulletin)
        {
            this.bulletin.Add(bulletin);
            this.bulletin.Save();
        }

        public Bulletin Get(string email)
        {
            return this.bulletin.All().Where(b => b.Email == email).FirstOrDefault();
        }

        public IQueryable<Bulletin> Get()
        {
            return this.bulletin.All();
        }
    }
}
