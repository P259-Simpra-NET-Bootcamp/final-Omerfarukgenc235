using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpraBitirme.BusinessLayer.Abstract;
using SimpraBitirme.BusinessLayer.Concrete;
using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.EntityLayer.Dto.Categories;
using SimpraBitirme.EntityLayer.Dto.Products;

namespace SimpraBitirme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _productService.GetList();
            return Ok(response);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var response = _productService.GetByID(id);
            return Ok(response);
        }
        [Authorize(Roles = "A")]
        [HttpPost]
        public ApiResponse Post([FromBody] ProductRequest product)
        {
            var result = _productService.Add(product);
            return result;
        }
        [Authorize(Roles = "A")]

        [HttpDelete]
        public IActionResult DeleteById(int id)
        {
            bool result = _productService.Delete(id);
            return Ok(result);
        }
        [Authorize(Roles = "A")]

        [HttpPut]
        public ApiResponse Update([FromBody] ProductResponse category)
        {
            var result = _productService.Update(category);
            return result;
        }
        [HttpGet("filter/Name")]
        public IActionResult GetListByFilterName(string Name)
        {
            var result = _productService.GetFilterByName(Name);
            return Ok(result);
        }

        [HttpPost("filter/Price")]
        public IActionResult GetListByFilterPrice([FromBody] ProductFilterRequest productFilterRequest)
        {
            var result = _productService.GetFilterByPrice(productFilterRequest);
            return Ok(result);
        }
    }
}
