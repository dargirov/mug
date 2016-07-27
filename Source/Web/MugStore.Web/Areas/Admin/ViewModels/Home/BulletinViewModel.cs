namespace MugStore.Web.Areas.Admin.ViewModels.Home
{
    using System.Collections.Generic;
    using Data.Models;

    public class BulletinViewModel
    {
        public IEnumerable<Bulletin> Bulletins { get; set; }
    }
}
