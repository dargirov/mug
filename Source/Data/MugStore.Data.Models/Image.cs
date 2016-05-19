namespace MugStore.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using MugStore.Data.Common.Models;
    using MugStore.Common;

    public class Image : BaseModel<int>
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
        public int Width { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public int Dpi { get; set; }

        public double Rotation { get; set; }

        public double Y { get; set; }

        public int? OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
