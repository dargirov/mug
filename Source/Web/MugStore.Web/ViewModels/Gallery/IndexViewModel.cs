﻿namespace MugStore.Web.ViewModels.Gallery
{
    using System.Collections.Generic;
    using Data.Models;

    public class IndexViewModel
    {
        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public string CategoryName { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }
}
