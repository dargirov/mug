namespace MugStore.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using MugStore.Common;
    using MugStore.Data.Common.Models;

    public class Category : BaseModel<int>
    {
        [Required]
        [MaxLength(GlobalConstants.MaxCategoryNameLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MaxCategoryAcronymLength)]
        public string Acronym { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}
