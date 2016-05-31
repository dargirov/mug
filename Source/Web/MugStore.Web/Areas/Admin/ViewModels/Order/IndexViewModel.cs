namespace MugStore.Web.Areas.Admin.ViewModels.Order
{
    using System.Collections.Generic;
    using Data.Models;

    public class IndexViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}
