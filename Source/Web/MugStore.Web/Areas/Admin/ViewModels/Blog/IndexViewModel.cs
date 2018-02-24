namespace MugStore.Web.Areas.Admin.ViewModels.Blog
{
    using System.Collections.Generic;
    using MugStore.Data.Models;

    public class IndexViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
    }
}