namespace MugStore.Web.ViewModels.Home
{
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class ContactsInputModel
    {
        [Required]
        [MaxLength(GlobalConstants.MaxFeedbackNameLength)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(GlobalConstants.EmailMaxLength)]
        public string Email { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(5)]
        public string Captcha { get; set; }
    }
}
