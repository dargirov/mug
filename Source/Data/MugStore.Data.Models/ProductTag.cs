namespace MugStore.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using MugStore.Common;
    using MugStore.Data.Common.Models;

    public class ProductTag : BaseModel<int>
    {
        [Required]
        [MaxLength(GlobalConstants.ProductTagMaxlength)]
        public string Name { get; set; }
    }
}
