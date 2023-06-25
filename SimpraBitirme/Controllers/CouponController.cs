using Azure;
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
            var response = _couponService.GetList();
            return Ok(response);
        }
        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var response = _couponService.GetByID(id);
            return Ok(response);
        }
        [Authorize(Roles = "A")]
        [HttpPost]
        public IActionResult Post([FromBody] CouponRequest couponRequest)
        {
            var response = _couponService.Add(couponRequest);
            return Ok(response);
        }

    }
}
