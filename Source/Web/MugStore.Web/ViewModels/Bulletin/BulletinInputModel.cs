namespace MugStore.Web.ViewModels.Bulletin
{
    using System.ComponentModel.DataAnnotations;
    using MugStore.Common;

    public class BulletinInputModel
    {
        [Required]
        [EmailAddress]
        [MaxLength(GlobalConstants.EmailMaxLength)]
        public string Email { get; set; }
    }
}
