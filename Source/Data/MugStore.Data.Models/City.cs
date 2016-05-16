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
        public int PostCode { get; set; }

        [Required]
        public bool Highlight { get; set; }
    }
}
