namespace MugStore.Web.ViewModels.Product
{
    using System.Collections.Generic;
    using MugStore.Data.Models;
    using MugStore.Web.Infrastructure.Mapping;

    public class IndexViewModel : IMapFrom<Product>
    {
        public string Title { get; set; }

        public string Acronym { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public virtual Category Category { get; set; }

        public string PreviewData { get; set; }

        public virtual IEnumerable<ProductImage> Images { get; set; }

        public virtual IEnumerable<ProductTag> Tags { get; set; }
    }
}
