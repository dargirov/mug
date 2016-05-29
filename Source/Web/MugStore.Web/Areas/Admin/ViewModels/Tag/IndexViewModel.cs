namespace MugStore.Web.Areas.Admin.ViewModels.Tag
{
    using System.Collections.Generic;
    using Data.Models;

    public class IndexViewModel
    {
        public IEnumerable<ProductTag> Tags { get; set; }
    }
}
