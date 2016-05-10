namespace MugStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MugStore.Data.Common.Models;

    public class Order : BaseModel<int>
    {
        public Order()
        {
            this.Images = new HashSet<Image>();
        }

        public virtual ICollection<Image> Images { get; set; }

        public int? Quantity { get; set; }

        public PaymentMethod? PaymentMethod { get; set; }

        public int DeliveryInfoId { get; set; }

        public virtual DeliveryInfo DeliveryInfo { get; set; }
    }
}
