using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpraBitirme.BusinessLayer.Abstract;
using SimpraBitirme.BusinessLayer.Concrete;

namespace SimpraBitirme.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var deger = _basketService.GetList();
            return Ok(deger);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var deger = _basketService.GetByID(id);
            return Ok(deger);
        }
        [HttpGet("GetByUser/{userId}")]
        public IActionResult GetByUserId(int userId)
        {
            var deger = _basketService.GetByUserId(userId);
            return Ok(deger);
        }
        [HttpPut("RemoveCouponCode")]
        public IActionResult RemoveCouponCode()
        {
            var response = _basketService.RemoveCouponCode();
            return Ok(response);
        }

        [HttpPut("UseCouponCode")]
        public IActionResult SetCouponCode(string couponCode)
        {
            var response = _basketService.UseCouponCode(couponCode);
            return Ok(response);
        }
    }
}
