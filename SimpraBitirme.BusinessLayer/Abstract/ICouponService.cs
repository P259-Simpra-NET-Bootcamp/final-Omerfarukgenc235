using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.EntityLayer.Dto.Categories;
using SimpraBitirme.EntityLayer.Dto.Coupons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.BusinessLayer.Abstract
{
    public interface ICouponService
    {
        List<CouponResponse> GetList();
        ApiResponse Add(CouponRequest coupon);
        CouponResponse GetByID(int id);
        CouponResponse GetByCode(string Code);
        List<CouponResponse> GetByUserId(int userId);
        bool Delete(int id);
    }
}
