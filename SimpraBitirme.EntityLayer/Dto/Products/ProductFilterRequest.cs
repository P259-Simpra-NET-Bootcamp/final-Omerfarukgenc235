using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.EntityLayer.Dto.Products
{
    public class ProductFilterRequest
    {
        public int MinPrice { get; set; }
        public int MaksPrice { get; set; }
    }
}
