using SimpraBitirme.EntityLayer.Concrete.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.EntityLayer.Concrete
{
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Tags { get; set; }
        public ICollection<CategoryProduct> CategoryProducts { get; set; }

    }
}
