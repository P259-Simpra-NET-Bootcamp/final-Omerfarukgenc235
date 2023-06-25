using Microsoft.EntityFrameworkCore;
using SimpraBitirme.DataAccessLayer.Abstract;
using SimpraBitirme.DataAccessLayer.Concrete;
using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Dto.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.DataAccessLayer.EntityFramework
{
    public class EfProductDal : Repository<Product>, IProductDal
    {
        private readonly DbContextOptions<Context> _options;
        private readonly IHttpContextAccessorService _httpContextAccessor;

        public EfProductDal(DbContextOptions<Context> options, IHttpContextAccessorService httpContextAccessorService) : base(new Context(options), httpContextAccessorService)
        {
            _options = options;
            _httpContextAccessor = httpContextAccessorService;
        }
        public List<ProductResponse> GetFilterByName(string name)
        {
            using (var context = new Context(_options))
            {
                return (from cs in context.Products
                        where cs.Name.Contains(name) || cs.Description.Contains(name)
                        orderby cs.Id ascending
                        select new ProductResponse
                        {
                            Id = cs.Id,
                            Name = cs.Name,
                            Activity = cs.Activity,
                            Description = cs.Description,
                            Price = cs.Price,
                            Tag = cs.Tag,
                            MaxPoint = cs.MaxPoint,
                            Property = cs.Property,
                            Quantity = cs.Quantity,
                            PointEarningPercentage = cs.PointEarningPercentage,
                            Url = cs.Url,
                        }).ToList();
            }
        }

        public List<ProductResponse> GetFilterByPrice(ProductFilterRequest productFilterRequest)
        {
            using (var context = new Context(_options))
            {
                return (from cs in context.Products
                        where cs.Price >= productFilterRequest.MinPrice && cs.Price <= productFilterRequest.MaksPrice
                        orderby cs.Id ascending
                        select new ProductResponse
                        {
                            Id = cs.Id,
                            Name = cs.Name,
                            Activity = cs.Activity,
                            Description = cs.Description,
                            Price = cs.Price,
                            Tag = cs.Tag,
                            MaxPoint = cs.MaxPoint,
                            Property = cs.Property,
                            Quantity = cs.Quantity,
                            PointEarningPercentage = cs.PointEarningPercentage,
                            Url = cs.Url,
                        }).ToList();
            }
        }
    }
}
