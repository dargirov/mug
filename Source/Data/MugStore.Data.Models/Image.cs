namespace MugStore.Data.Models
{
    using System;
    using MugStore.Data.Common.Models;

    public class Image : BaseModel<int>
    {
        public string Name { get; set; }

        public string ContentType { get; set; }

        public string Path { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Dpi { get; set; }

        public double Rotation { get; set; }

        public double Y { get; set; }

        public int? OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
