namespace SimpraBitirme.EntityLayer.Concrete.Dto.BasketDto
{
    public class BasketDto
    {
        public int UserId { get; set; }
        public string? DiscountCode { get; set; }
        public int? DiscountRate { get; set; }
        public List<BasketItemDto> basketItems { get; set; }
        public decimal TotalPrice { get; set; }       
    }
}