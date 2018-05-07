namespace MugStore.Services.Data
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using MugStore.Data.Models;

    public interface IBlogService
    {
        IQueryable<Post> GetPosts(Expression<Func<Post, bool>> predicate);

        Post GetPost(string acronym);

        Post GetPost(int id);

        void Create(Post post);

        void Save();
    }
}
