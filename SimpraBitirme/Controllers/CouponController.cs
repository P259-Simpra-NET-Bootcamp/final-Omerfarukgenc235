using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpraBitirme.BusinessLayer.Abstract;
using SimpraBitirme.EntityLayer.Dto.Coupons;

namespace SimpraBitirme.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var deger = _couponService.GetList();
            return Ok(deger);
        }
        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var deger = _couponService.GetByID(id);
            return Ok(deger);
        }
        [Authorize(Roles = "A")]
        [HttpPost]
        public IActionResult Post([FromBody] CouponRequest couponRequest)
        {
            var deger = _couponService.Add(couponRequest);
            return Ok(deger);
        }

    }
}
