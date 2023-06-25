using SimpraBitirme.EntityLayer.Concrete.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.EntityLayer.Concrete
{
    public class Coupon : BaseModel
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public string Code { get; set; }
        public double Price { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool Status { get; set; }
    }
}
