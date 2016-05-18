namespace MugStore.Services.Data
{
    using System.Linq;
    using MugStore.Data.Common;
    using MugStore.Data.Models;

    public class BulletinsService : IBulletinsService
    {
        private readonly IDbRepository<Bulletin> bulletins;

        public BulletinsService(IDbRepository<Bulletin> bulletins)
        {
            this.bulletins = bulletins;
        }

        public void Add(Bulletin bulletin)
        {
            this.bulletins.Add(bulletin);
            this.bulletins.Save();
        }

        public Bulletin Get(string email)
        {
            return this.bulletins.All().Where(b => b.Email == email).FirstOrDefault();
        }
    }
}
