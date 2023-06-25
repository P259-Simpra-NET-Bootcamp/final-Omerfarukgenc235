using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.EntityLayer.Dto.Products;

namespace SimpraBitirme.BusinessLayer.Abstract
{
    public interface IProductService
    {
        List<ProductResponse> GetList();
        ApiResponse Add(ProductRequest product);
        List<ProductResponse> GetFilterByName(string name);
        List<ProductResponse> GetFilterByPrice(ProductFilterRequest productFilterRequest);
        ProductResponse GetByID(int id);
        bool Delete(int id);
        ApiResponse Update(ProductResponse product);
    }
}
