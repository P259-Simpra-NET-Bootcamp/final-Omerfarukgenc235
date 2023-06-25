using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.EntityLayer.Dto.Coupons
{
    public class CouponRequest
    {
        public int UserId { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
