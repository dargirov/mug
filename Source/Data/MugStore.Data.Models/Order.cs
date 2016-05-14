namespace MugStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;
    using MugStore.Common;

    public class Order : BaseModel<int>
    {
        public Order()
        {
            this.Images = new HashSet<Image>();
        }

        public virtual ICollection<Image> Images { get; set; }

        [Required]
        [Range(1, GlobalConstants.MaxOrderQuantity)]
        public int Quantity { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        public int DeliveryInfoId { get; set; }

        [Required]
        public virtual DeliveryInfo DeliveryInfo { get; set; }
    }
}
