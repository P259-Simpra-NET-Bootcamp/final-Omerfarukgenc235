using SimpraBitirme.EntityLayer.Dto.OrderItems;

namespace SimpraBitirme.EntityLayer.OrderDto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string BuyerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
