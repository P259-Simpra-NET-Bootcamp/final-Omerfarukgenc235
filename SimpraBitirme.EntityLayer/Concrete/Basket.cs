using SimpraBitirme.EntityLayer.Concrete.Base;

namespace SimpraBitirme.EntityLayer.Concrete
{
    public class Basket : BaseModel
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string? CouponCode { get; set; }
        public int? CouponPrice { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public double TotalPrice
        {
            get => BasketItems.Sum(x => x.Product.Price * x.Quantity);
        }
    }
}