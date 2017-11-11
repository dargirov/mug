namespace MugStore.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using Data.Models;

    public class IndexViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public string MugInfoType { get; set; }
    }
}
