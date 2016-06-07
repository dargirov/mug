namespace MugStore.Web.Areas.Admin.ViewModels.Product
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Data.Models;
    using Infrastructure.Mapping;

    public class CreateViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

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

        public IEnumerable<Category> Categories { get; set; }

        public string PreviewData { get; set; }

        public virtual ICollection<ProductImage> Images { get; set; }

        public virtual IEnumerable<ProductTag> Tags { get; set; }

        public virtual IEnumerable<ProductTag> AllTags { get; set; }
    }
}
