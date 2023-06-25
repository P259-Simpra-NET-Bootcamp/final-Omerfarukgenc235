using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Dto.BasketDto;
using SimpraBitirme.EntityLayer.Dto.CategoryProduct;

namespace SimpraBitirme.DataAccessLayer.Abstract
{
    public interface IBasketDal : IRepository<Basket>
    {
        List<BasketResponse> GetAll();
        BasketResponse GetById(int id);
        BasketResponse GetByUserId(int userId);
    }
}
