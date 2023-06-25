using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpraBitirme.BusinessLayer.Abstract;
using SimpraBitirme.BusinessLayer.Concrete;
using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.EntityLayer.Concrete.Dto.BasketDto;
using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Dto.OrderItems;
using Microsoft.AspNetCore.Authorization;

namespace SimpraBitirme.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }
        [HttpGet("Id")]
        public ApiResponse<OrderItemResponse> GetById(int Id)
        {
            var result = _orderItemService.GetByID(Id);
            return result;
        }
        [HttpPost]
        public ApiResponse Post([FromBody] OrderItemRequest orderItem)
        {
            var result = _orderItemService.Add(orderItem);
            return result;
        }
        [HttpGet()]
        public ApiResponse<List<OrderItemResponse>> GetAll()
        {
            var result = _orderItemService.GetList();
            return result;
        }
    }
}
