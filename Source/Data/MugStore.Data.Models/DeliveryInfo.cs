namespace MugStore.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using MugStore.Data.Common.Models;
    using MugStore.Common;

    public class DeliveryInfo : BaseModel<int>
    {
        [Required]
        [MaxLength(GlobalConstants.MaxDeliveryInfoFullNameLength)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MaxDeliveryInfoPhoneLength)]
        public string Phone { get; set; }

        [Required]
        public int CityId { get; set; }

        public virtual City City { get; set; }

        [Required]
        public string Address { get; set; }

        public string Comment { get; set; }
    }
}
