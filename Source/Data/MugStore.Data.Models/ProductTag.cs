namespace MugStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;
    using MugStore.Common;

    public class ProductTag : BaseModel<int>
    {
        public ProductTag()
        {
            this.Products = new HashSet<Product>();
        }

        [Required]
        [MaxLength(GlobalConstants.ProductTagMaxlength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(GlobalConstants.ProductTagAcronymMaxlength)]
        public string Acronym { get; set; }

        [Required]
        public bool Active { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
