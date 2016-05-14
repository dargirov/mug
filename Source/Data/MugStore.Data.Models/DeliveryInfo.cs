namespace MugStore.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using MugStore.Data.Common.Models;

    public class DeliveryInfo : BaseModel<int>
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }

        [Required]
        public int CityId { get; set; }

        public virtual City City { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}
