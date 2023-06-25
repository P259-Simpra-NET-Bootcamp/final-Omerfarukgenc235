using SimpraBitirme.EntityLayer.Concrete.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.EntityLayer.Concrete
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Property { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Tag { get; set; }
        public bool Activity { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public float PointEarningPercentage { get; set; }
        public int MaxPoint { get; set; }
        public ICollection<CategoryProduct> CategoryProducts { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
      
    }
}