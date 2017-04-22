namespace MugStore.Data.Models
{
    using MugStore.Common;
    using MugStore.Data.Common.Models;
    using System.ComponentModel.DataAnnotations;

    public class Courier : BaseModel<int>
    {
        [Required]
        [MaxLength(GlobalConstants.MaxCourierNameLength)]
        public string Name { get; set; }

        [Required]
        public bool Active { get; set; }

        public string Info { get; set; }
    }
}
