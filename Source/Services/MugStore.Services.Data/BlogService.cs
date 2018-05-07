namespace MugStore.Services.Data
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using MugStore.Data.Common;
    using MugStore.Data.Models;

    public class BlogService : IBlogService
    {
        private readonly IDbRepository<Post> posts;

        public BlogService(IDbRepository<Post> posts)
        {
            this.posts = posts;
        }

        public IQueryable<Post> GetPosts(Expression<Func<Post, bool>> predicate)
        {
            return this.posts.All().Where(predicate).OrderByDescending(x => x.CreatedOn);
        }

        public Post GetPost(string acronym)
        {
            return this.posts.All().Where(x => x.Acronym == acronym).FirstOrDefault();
        }

        public Post GetPost(int id)
        {
            return this.posts.GetById(id);
        }

        public void Create(Post post)
        {
            this.posts.Add(post);
            this.posts.Save();
        }

        public void Save()
        {
            this.posts.Save();
        }
    }
}
