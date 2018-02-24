namespace MugStore.Web.Areas.Admin.ViewModels.Tag
{
    using System.Collections.Generic;
    using Data.Models;

    public class IndexViewModel
    {
        public IEnumerable<ProductTag> ProductTags { get; set; }

        public IEnumerable<PostTag> PostTags { get; set; }
    }
}
