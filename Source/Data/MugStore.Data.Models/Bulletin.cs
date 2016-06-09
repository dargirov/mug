namespace MugStore.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using MugStore.Common;
    using Common.Models;

    public class Bulletin : BaseModel<int>
    {
        [Required]
        [Index(IsUnique = true)]
        [MaxLength(GlobalConstants.EmailMaxLength)]
        public string Email { get; set; }

        public bool Verified { get; set; }
    }
}
