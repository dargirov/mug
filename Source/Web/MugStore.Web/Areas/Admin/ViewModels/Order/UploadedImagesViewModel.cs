namespace MugStore.Web.Areas.Admin.ViewModels.Order
{
    using System.Collections.Generic;
    using MugStore.Data.Models;

    public class UploadedImagesViewModel
    {
        public IEnumerable<Image> Images { get; set; }
    }
}
