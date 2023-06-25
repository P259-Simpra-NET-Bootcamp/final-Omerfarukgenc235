using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.EntityLayer.Dto.Categories;
using SimpraBitirme.EntityLayer.Dto.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.BusinessLayer.Abstract
{
    public interface IOrderService
    {
        List<OrderResponse> GetList();
        ApiResponse Add(OrderRequest category);
        OrderResponse GetByID(int id);
        List<OrderResponse> GetListByUserId();
    }
}
