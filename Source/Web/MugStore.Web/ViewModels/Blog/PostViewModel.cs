namespace MugStore.Web.ViewModels.Blog
{
    using System;
    using System.Collections.Generic;
    using MugStore.Data.Models;
    using MugStore.Web.Infrastructure.Mapping;

    public class PostViewModel : IMapFrom<Post>
    {
        public string Title { get; set; }

        public string PageTitle { get; set; }

        public string PageDescription { get; set; }

        public string Acronym { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual IEnumerable<PostTag> Tags { get; set; }
    }
}