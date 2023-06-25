using SimpraBitirme.EntityLayer.Models;

namespace SimpraBitirme.EntityLayer.Dto.Categories
{
    public class CategoryRequest : BaseRequest
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Tags { get; set; }
    }
}
