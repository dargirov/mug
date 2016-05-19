namespace MugStore.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using MugStore.Common;
    using MugStore.Data.Common.Models;

    public class ProductImage : BaseModel<int>
    {
        [Required]
        [MaxLength(GlobalConstants.MaxImagePathLength)]
        public string Path { get; set; }
    }
}
