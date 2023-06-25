using SimpraBitirme.EntityLayer.Concrete;

namespace SimpraBitirme.EntityLayer.Dto.BasketDto
{
    public class BasketResponse
    {      
        public int UserId { get; set; }
        public string? CouponCode { get; set; }
        public int? CouponPrice { get; set; }
        public List<BasketItemResponse> BasketItems { get; set; }
        public double TotalPrice
        {
            get => BasketItems.Sum(x => x.ProductResponse.Price * x.Quantity);
        }
        public BasketResponse()
        {
            BasketItems = new List<BasketItemResponse>();
        }
    }
}