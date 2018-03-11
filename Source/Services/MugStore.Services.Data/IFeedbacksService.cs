namespace MugStore.Services.Data
{
    using System.Linq;
    using MugStore.Data.Models;

    public interface IFeedbacksService
    {
        IQueryable<Feedback> Get();

        void Add(Feedback feedback);

        void Save();

        void Delete(Feedback feedback);
    }
}
