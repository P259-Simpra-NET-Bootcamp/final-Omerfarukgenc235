using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Dto.BasketDto;
using SimpraBitirme.EntityLayer.Dto.OrderDto;

namespace SimpraBitirme.DataAccessLayer.Abstract
{
    public interface IOrderDal : IRepository<Order>
    {
        List<OrderResponse> GetAll();
        OrderResponse GetById(int id);
        List<OrderResponse> GetByUserId(int userId);

    }
}
