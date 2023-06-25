using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Concrete.Dto.BasketDto;
using SimpraBitirme.EntityLayer.Dto.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.BusinessLayer.Abstract
{
    public interface IBasketItemService
    {
        ApiResponse<List<BasketItem>> GetListByBasketId(int basketId);    
        ApiResponse Add(BasketItemDto basketItem);
        ApiResponse Delete(int id);
    }
}