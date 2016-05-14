namespace MugStore.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class City : BaseModel<int>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string PostCode { get; set; }
    }
}
