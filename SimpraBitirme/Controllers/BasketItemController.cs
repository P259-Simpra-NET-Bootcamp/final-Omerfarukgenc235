using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpraBitirme.BusinessLayer.Abstract;
using SimpraBitirme.BusinessLayer.Concrete;
using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Concrete.Dto.BasketDto;
using SimpraBitirme.EntityLayer.Dto.Categories;

namespace SimpraBitirme.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketItemController : ControllerBase
    {
        private readonly IBasketItemService _basketItemService;

        public BasketItemController(IBasketItemService basketItemService)
        {
            _basketItemService = basketItemService;
        }
        [HttpGet("Id")]
        public ApiResponse<List<BasketItem>> GetAllByBasketId(int Id)
        {
            var result = _basketItemService.GetListByBasketId(Id);
            return result;
        }
        [HttpPost]
        public ApiResponse Post([FromBody] BasketItemDto basketItem)
        {
            var result = _basketItemService.Add(basketItem);
            return result;
        }
        [HttpDelete("Id")]
        public ApiResponse Delete(int Id)
        {
            var result = _basketItemService.Delete(Id);
            return result;
        }
    }
}
