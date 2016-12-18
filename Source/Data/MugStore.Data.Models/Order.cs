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
        [MaxLength(GlobalConstants.OrderAcronymLength)]
        public string Acronym { get; set; }

        [Required]
        [Range(1, GlobalConstants.MaxOrderQuantity)]
        public int Quantity { get; set; }

        [Required]
        public decimal PriceCustomer { get; set; }

        [Required]
        public decimal PriceSupplier { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public int DeliveryInfoId { get; set; }

        public virtual DeliveryInfo DeliveryInfo { get; set; }

        [Required]
        public virtual OrderStatus OrderStatus { get; set; }

        public virtual ConfirmationStatus ConfirmationStatus { get; set; }

        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
