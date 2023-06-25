using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.EntityLayer.Dto.Categories;
using SimpraBitirme.EntityLayer.Dto.OrderItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.BusinessLayer.Abstract
{
    public interface IOrderItemService
    {
        ApiResponse<List<OrderItemResponse>> GetList();
        ApiResponse Add(OrderItemRequest orderItem);
        ApiResponse<OrderItemResponse> GetByID(int id);    
    }
}
