namespace MugStore.Web.Areas.Admin.ViewModels.Product
{
    using System.Collections.Generic;
    using MugStore.Data.Models;

    public class IndexViewModel
    {
        public IEnumerable<Product> Products { get; set; }
    }
}
