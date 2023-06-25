using SimpraBitirme.EntityLayer.Concrete.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.EntityLayer.Concrete
{
    public class OrderItem : BaseModel
    {
        public int OrderId { get;  set; }
        public Order Order { get;  set; }
        public int ProductId { get;  set; }
        public double ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductName { get; set; }
    }
}