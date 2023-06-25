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
            var response = _basketService.GetList();
            return Ok(response);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var response = _basketService.GetByID(id);
            return Ok(response);
        }
        [HttpGet("GetByUser/{userId}")]
        public IActionResult GetByUserId(int userId)
        {
            var response = _basketService.GetByUserId(userId);
            return Ok(response);
        }

        [HttpGet("GetMyBasket")]
        public IActionResult GetMyBasket()
        {
            var response = _basketService.GetByAuthenticationUserBasket();
            return Ok(response);
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
