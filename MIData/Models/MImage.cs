using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MIData.Models
{
    public class MImage
    {
        public int ID { get; set; }

        public string Path { get; set; }

        public string Size { get; set; }

        public decimal Price { get; set; }

        public int Copies { get; set; }
    }

    public class MImages
    {
        public List<MImage> MImageList { get; set; }
        public string Folder { get; set; }
        public decimal Total { get; set; }
    }
}