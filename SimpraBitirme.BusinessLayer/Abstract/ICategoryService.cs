using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Dto.Categories;

namespace SimpraBitirme.BusinessLayer.Abstract
{
    public interface ICategoryService
    {
        List<CategoryResponse> GetList();
        ApiResponse Add(CategoryRequest category);
        CategoryResponse GetByID(int id);
        bool Delete(int id);
        ApiResponse Update(CategoryRequest category);
    }
}
