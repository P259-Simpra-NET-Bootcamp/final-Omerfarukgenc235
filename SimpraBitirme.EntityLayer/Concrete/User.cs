using SimpraBitirme.EntityLayer.Concrete.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.EntityLayer.Concrete
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public double PointBalance { get; set; }
        public List<Coupon> Coupon { get; set; }
        public List<Order> Order { get; set; }
        public List<Basket> Basket { get; set; }
    }
}