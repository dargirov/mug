namespace MugStore.Web.Areas.Admin.ViewModels.Blog
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using MugStore.Common;
    using MugStore.Data.Models;
    using MugStore.Web.Infrastructure.Mapping;

    public class CreateViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MaxBlogPostTitleLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MaxBlogPostTitleLength)]
        public string PageTitle { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MaxBlogPostTitleLength)]
        public string PageDescription { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MaxBlogPostAcronymLength)]
        public string Acronym { get; set; }

        [Required]
        [AllowHtml]
        public string ShortDescription { get; set; }

        [Required]
        [AllowHtml]
        public string FullDescription { get; set; }

        public IEnumerable<PostTag> Tags { get; set; }

        public IEnumerable<PostTag> AllTags { get; set; }

        public bool Active { get; set; }
    }
}