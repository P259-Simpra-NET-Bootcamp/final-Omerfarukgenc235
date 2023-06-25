using SimpraBitirme.EntityLayer.Concrete.Base;

namespace SimpraBitirme.EntityLayer.Concrete
{
    public class BasketItem : BaseModel
    {
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }      
    }
}