using SimpraBitirme.EntityLayer.Concrete.Base;

namespace SimpraBitirme.EntityLayer.Concrete
{
    public class CategoryProduct : BaseModel
    {
        public int CategoryId { get; set; }
        public Category Category{ get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
