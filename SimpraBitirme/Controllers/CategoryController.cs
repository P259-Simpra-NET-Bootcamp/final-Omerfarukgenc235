using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpraBitirme.BusinessLayer.Abstract;
using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.EntityLayer.Dto.Categories;

namespace SimpraOdev2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _categoryService.GetList();
            return Ok(response);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var response = _categoryService.GetByID(id);
            return Ok(response);
        }
        [Authorize(Roles = "A")]
        [HttpPost]
        public ApiResponse Post([FromBody] CategoryRequest category)
        {
            var result = _categoryService.Add(category);
            return result;
        }
        [HttpDelete]
        public IActionResult DeleteById(int id)
        {
            bool result = _categoryService.Delete(id);
            return Ok(result);
        }
        [Authorize(Roles = "A")]
        [HttpPut]
        public ApiResponse Update([FromBody] CategoryRequest category)
        {
            var result = _categoryService.Update(category);
            return result;
        }
    }
}
