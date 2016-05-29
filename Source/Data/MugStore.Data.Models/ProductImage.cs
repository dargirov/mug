namespace MugStore.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Common.Models;
    using MugStore.Common;

    public class ProductImage : BaseModel<int>
    {
        [Required]
        [MaxLength(GlobalConstants.MaxImageNameLength)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MaxImageOriginalNameLength)]
        public string OriginalName { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MaxImageContentTypeLength)]
        public string ContentType { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MaxImagePathLength)]
        public string Path { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}
