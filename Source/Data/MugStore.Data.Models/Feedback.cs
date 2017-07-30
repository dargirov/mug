namespace MugStore.Data.Models
{
    using MugStore.Common;
    using MugStore.Data.Common.Models;
    using System.ComponentModel.DataAnnotations;

    public class Feedback : BaseModel<int>
    {
        [Required]
        [MaxLength(GlobalConstants.MaxFeedbackNameLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(GlobalConstants.EmailMaxLength)]
        public string Email { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public bool IsNew { get; set; }
    }
}
