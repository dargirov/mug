namespace MugStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MugStore.Common;
    using MugStore.Data.Common.Models;

    public class PostTag : BaseModel<int>
    {
        public PostTag()
        {
            this.Posts = new HashSet<Post>();
        }

        [Required]
        [MaxLength(GlobalConstants.PostTagMaxlength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(GlobalConstants.PostTagAcronymMaxlength)]
        public string Acronym { get; set; }

        [Required]
        public bool Active { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
