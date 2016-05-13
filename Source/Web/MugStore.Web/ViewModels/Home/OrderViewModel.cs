namespace MugStore.Web.ViewModels.Home
{
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class OrderViewModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public int Quantity { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public virtual DeliveryInfo DeliveryInfo { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            //configuration.CreateMap<Order, OrderViewModel>()
            //    .ForMember(x => x.DeliveryInfo, opt => opt.MapFrom(x => x.DeliveryInfo + " " + x.User.LastName + " (" + x.User.Email + ")"));
        }
    }
}
