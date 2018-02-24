namespace MugStore.Web.Areas.Admin.ViewModels.Tag
{
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class CreateInputModel
    {
        [Required]
        [MaxLength(GlobalConstants.ProductTagMaxlength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(GlobalConstants.ProductTagAcronymMaxlength)]
        public string Acronym { get; set; }

        [Required]
        public bool IsProductTag { get; set; }
    }
}
