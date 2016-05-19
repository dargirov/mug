namespace MugStore.Web.ViewModels.Gallery
{
    using System.Collections.Generic;
    using MugStore.Data.Models;

    public class IndexViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}