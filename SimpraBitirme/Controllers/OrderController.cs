using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpraBitirme.BusinessLayer.Abstract;
using SimpraBitirme.BusinessLayer.Concrete;
using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.EntityLayer.Concrete.Dto.BasketDto;
using SimpraBitirme.EntityLayer.Dto.OrderDto;

namespace SimpraBitirme.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var deger = _orderService.GetList();
            return Ok(deger);
        }
        [HttpGet("report/GetByUserId")]
        public IActionResult GetByUserId()
        {
            var deger = _orderService.GetListByUserId();
            return Ok(deger);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var deger = _orderService.GetByID(id);
            return Ok(deger);
        }
        [HttpPost]
        public ApiResponse Post([FromBody] OrderRequest orderRequest)
        {
            var result = _orderService.Add(orderRequest);
            return result;
        }
    }
}