namespace MugStore.Web.ViewModels.Order
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using MugStore.Common;

    public class CreateInputModel /*: IMapFrom<Order>, IHaveCustomMappings*/
    {
        [Required]
        [Range(1, GlobalConstants.MaxOrderQuantity)]
        public int Quantity { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        public DeliveryInfo DeliveryInfo { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            //configuration.CreateMap<Order, OrderViewModel>()
            //    .ForMember(x => x.DeliveryInfo, opt => opt.MapFrom(x => x.DeliveryInfo + " " + x.User.LastName + " (" + x.User.Email + ")"));
        }
    }
}
