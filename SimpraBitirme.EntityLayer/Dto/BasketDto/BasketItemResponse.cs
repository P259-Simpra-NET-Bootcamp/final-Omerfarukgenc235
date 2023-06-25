using SimpraBitirme.EntityLayer.Dto.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.EntityLayer.Dto.BasketDto
{
    public class BasketItemResponse
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public ProductResponse ProductResponse { get; set; }
    }
}
