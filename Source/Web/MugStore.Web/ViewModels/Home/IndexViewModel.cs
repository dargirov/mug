namespace MugStore.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using MugStore.Data.Models;

    public class IndexViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<City> Cities { get; set; }
    }
}
