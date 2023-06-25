using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Dto.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.DataAccessLayer.Abstract
{
    public interface IProductDal : IRepository<Product>
    {
        List<ProductResponse> GetFilterByName(string name);
        List<ProductResponse> GetFilterByPrice(ProductFilterRequest productFilterRequest);
    }
}
