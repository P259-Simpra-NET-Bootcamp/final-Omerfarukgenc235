using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.EntityLayer.Dto.Coupons
{
    public class CouponResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; }
        public int Price { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool Status { get; set; }
    }
}
