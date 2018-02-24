namespace MugStore.Services.Data
{
    using System.Linq;
    using MugStore.Data.Models;

    public interface IBlogService
    {
        IQueryable<Post> GetPosts();

        Post GetPost(string acronym);

        Post GetPost(int id);

        void Create(Post post);

        void Save();
    }
}
