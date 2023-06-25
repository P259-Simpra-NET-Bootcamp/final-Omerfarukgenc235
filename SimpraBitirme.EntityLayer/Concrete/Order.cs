using SimpraBitirme.EntityLayer.Concrete.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.EntityLayer.Concrete
{
    public class Order : BaseModel
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string OrderNumber { get; set; }
        public string? CouponCode { get; set; }
        public double? CouponPrice{ get; set; }
        public double? PointPrice{ get; set; }
        public double TotalPrice { get; set; }
        public List<OrderItem> OrderItem { get; set; }
    }
}