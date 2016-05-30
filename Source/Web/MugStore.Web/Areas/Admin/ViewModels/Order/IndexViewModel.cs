namespace MugStore.Web.Areas.Admin.ViewModels.Order
{
    using System.Collections.Generic;
    using MugStore.Common;
    using MugStore.Data.Models;

    public class IndexViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}
