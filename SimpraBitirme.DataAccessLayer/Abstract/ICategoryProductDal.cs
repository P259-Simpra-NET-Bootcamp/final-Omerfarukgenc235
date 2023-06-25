using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Dto.CategoryProduct;

namespace SimpraBitirme.DataAccessLayer.Abstract
{
    public interface ICategoryProductDal : IRepository<CategoryProduct>
    {
        List<CategoryProductResponse> GetAll();
        CategoryProductResponse GetById(int id);

    }
}
