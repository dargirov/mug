namespace MugStore.Web.Areas.Admin.ViewModels.Category
{
    using System.Collections.Generic;
    using Data.Models;

    public class IndexViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}
