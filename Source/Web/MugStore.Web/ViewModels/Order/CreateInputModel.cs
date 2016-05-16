namespace MugStore.Web.ViewModels.Order
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using MugStore.Common;

    public class CreateInputModel
    {
        [Required]
        [Range(1, GlobalConstants.MaxOrderQuantity)]
        public int Quantity { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        public DeliveryInfo DeliveryInfo { get; set; }

        public List<ImageInputModel> Images { get; set; }
    }
}
