using SimpraBitirme.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.EntityLayer.Dto.OrderItems
{
    public class OrderItemRequest
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
