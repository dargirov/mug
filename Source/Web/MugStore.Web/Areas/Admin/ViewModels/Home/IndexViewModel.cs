namespace MugStore.Web.Areas.Admin.ViewModels.Home
{
    using System.Collections.Generic;
    using Data.Models;

    public class IndexViewModel
    {
        public IEnumerable<Order> Orders { get; set; }

        public IEnumerable<Bulletin> Bulletin { get; set; }

        public IEnumerable<Image> Images { get; set; }

        public IEnumerable<Order> PriceChartOrders { get; set; }
    }
}
