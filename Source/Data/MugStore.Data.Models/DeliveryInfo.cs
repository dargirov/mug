namespace MugStore.Data.Models
{
    using MugStore.Data.Common.Models;

    public class DeliveryInfo : BaseModel<int>
    {
        public string FullName { get; set; }

        public string Phone { get; set; }

        public int? CityId { get; set; }

        public virtual City City { get; set; }

        public string Address { get; set; }

        public string Comment { get; set; }
    }
}
