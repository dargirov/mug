namespace MugStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;
    using MugStore.Common;

    public class Product : BaseModel<int>
    {
        public Product()
        {
            this.Images = new HashSet<ProductImage>();
            this.Tags = new HashSet<ProductTag>();
        }

        [Required]
        [MaxLength(GlobalConstants.MaxProductTitleLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MaxProductAcronymLength)]
        public string Acronym { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MaxProductCodeLength)]
        public string Code { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string PreviewData { get; set; }

        public virtual ICollection<ProductImage> Images { get; set; }

        public virtual ICollection<ProductTag> Tags { get; set; }

        [MaxLength(GlobalConstants.LinkToProductImageMaxLength)]
        public string LinkToSource { get; set; }
    }
}
