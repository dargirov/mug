namespace MugStore.Data.Models
{
    using MugStore.Data.Common.Models;

    public class City : BaseModel<int>
    {
        public string Name { get; set; }

        public string PostCode { get; set; }
    }
}
