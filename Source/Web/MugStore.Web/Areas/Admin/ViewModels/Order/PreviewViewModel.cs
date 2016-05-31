namespace MugStore.Web.Areas.Admin.ViewModels.Order
{
    using System.Collections.Generic;
    using Data.Models;
    using Infrastructure.Mapping;

    public class PreviewViewModel : IMapFrom<Order>
    {
        public string Acronym { get; set; }

        public IEnumerable<Image> Images { get; set; }
    }
}
