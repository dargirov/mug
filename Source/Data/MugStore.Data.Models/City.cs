namespace MugStore.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Models;
    using MugStore.Common;

    public class City : BaseModel<int>
    {
        [Required]
        [MaxLength(GlobalConstants.MaxCityNameLength)]
        public string Name { get; set; }

        [Required]
        public int PostCode { get; set; }

        [Required]
        public CityType Type { get; set; }

        [Required]
        public bool Highlight { get; set; }
    }
}
