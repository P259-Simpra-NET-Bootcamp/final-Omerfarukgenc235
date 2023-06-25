using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.EntityLayer.Dto.Categories;
using SimpraBitirme.EntityLayer.Dto.CategoryProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.BusinessLayer.Abstract
{
    public interface ICategoryProductService
    {
        List<CategoryProductResponse> GetList();
        List<CategoryProductResponse> GetListByCategoryId(int categoryId);
        CategoryProductResponse GetByID(int id);
        ApiResponse Add(CategoryProductRequest categoryProduct);
        bool Delete(int id);
    }
}
