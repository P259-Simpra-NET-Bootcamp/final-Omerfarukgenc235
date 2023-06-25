using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.EntityLayer.Dto.BasketDto;
using SimpraBitirme.EntityLayer.Dto.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.BusinessLayer.Abstract
{
    public interface IBasketService
    {
        List<BasketResponse> GetList();
        BasketResponse GetByUserId(int userId);
        BasketResponse GetByAuthenticationUserBasket();
        ApiResponse Add(BasketInsertRequest basketInsertRequest);
        BasketResponse GetByID(int id);
        ApiResponse UseCouponCode(string couponCode);
        ApiResponse RemoveCouponCode();
        bool Delete(int id);
    }
}