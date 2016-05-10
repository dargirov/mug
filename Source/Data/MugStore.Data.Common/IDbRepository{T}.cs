namespace MugStore.Data.Common
{
    using System.Linq;
    using MugStore.Data.Common.Models;

    public interface IDbRepository<T> : IDbRepository<T, int> where T : BaseModel<int>
    {
    }

    public interface IDbRepository<T, in TKey> where T : BaseModel<int>
    {
        IQueryable<T> All();

        IQueryable<T> AllWithDeleted();

        T GetById(TKey id);

        void Add(T entity);

        void Delete(T entity);

        void HardDelete(T entity);

        void Save();
    }
}
