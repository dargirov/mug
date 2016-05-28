namespace MugStore.Web.ViewModels.Home
{
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class ContactsInputModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(GlobalConstants.EmailMaxLength)]
        public string Email { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}
