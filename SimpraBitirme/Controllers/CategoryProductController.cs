using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpraBitirme.BusinessLayer.Abstract;
using SimpraBitirme.BusinessLayer.Concrete;
using SimpraBitirme.EntityLayer.Dto.CategoryProduct;
using SimpraBitirme.EntityLayer.Dto.Coupons;

namespace SimpraBitirme.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryProductController : ControllerBase
    {
        private readonly ICategoryProductService _categoryProductService;

        public CategoryProductController(ICategoryProductService categoryProductService)
        {
            _categoryProductService = categoryProductService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var deger = _categoryProductService.GetList();
            return Ok(deger);
        }
        [HttpGet("Id")]
        public IActionResult GetById(int Id)
        {
            var deger = _categoryProductService.GetByID(Id);
            return Ok(deger);
        }
        [Authorize(Roles = "A")]
        [HttpPost]
        public IActionResult Post([FromBody] CategoryProductRequest categoryProductRequest)
        {
            var deger = _categoryProductService.Add(categoryProductRequest);
            return Ok(deger);
        }
        [HttpGet("categoryFilter/{catgoryId}")]
        public IActionResult GetByFilterCategoryId(int catgoryId)
        {
            var deger = _categoryProductService.GetListByCategoryId(catgoryId);
            return Ok(deger);
        }

    }
}
