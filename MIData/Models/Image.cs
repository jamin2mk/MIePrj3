using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MIData.Models
{
     public class Image
    {
        public HttpPostedFileBase Images { get; set; }
    }
}
