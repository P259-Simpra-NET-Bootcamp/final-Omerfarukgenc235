using SimpraBitirme.EntityLayer.Dto.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.EntityLayer.Dto.OrderItems
{
    public class OrderItemResponse
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public double ProductQuantity { get; set; }
    }
}
