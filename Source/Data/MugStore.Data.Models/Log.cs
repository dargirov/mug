namespace MugStore.Data.Models
{
    using MugStore.Data.Common.Models;
    using System.ComponentModel.DataAnnotations;

    public class Log : BaseModel<int>
    {
        [Required]
        public LogLevel Level { get; set; }

        [MaxLength(100)]
        public string Code { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
