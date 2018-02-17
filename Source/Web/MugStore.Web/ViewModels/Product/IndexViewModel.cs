namespace MugStore.Web.ViewModels.Product
{
    using System.Collections.Generic;
    using Data.Models;
    using Infrastructure.Mapping;

    public class IndexViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Acronym { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public virtual Category Category { get; set; }

        public string PreviewData { get; set; }

        public virtual IEnumerable<ProductImage> Images { get; set; }

        public virtual IEnumerable<ProductTag> Tags { get; set; }

        public string PageTitle { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
