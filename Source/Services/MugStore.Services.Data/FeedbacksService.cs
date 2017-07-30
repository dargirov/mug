namespace MugStore.Services.Data
{
    using System.Linq;
    using MugStore.Data.Common;
    using MugStore.Data.Models;

    public class FeedbacksService : IFeedbacksService
    {
        private readonly IDbRepository<Feedback> feedback;

        public FeedbacksService(IDbRepository<Feedback> feedback)
        {
            this.feedback = feedback;
        }

        public void Add(Feedback feedback)
        {
            this.feedback.Add(feedback);
            this.feedback.Save();
        }

        public IQueryable<Feedback> Get()
        {
            return this.feedback.All();
        }

        public void Save()
        {
            this.feedback.Save();
        }
    }
}
