using Microsoft.EntityFrameworkCore;
using SimpraBitirme.DataAccessLayer.Abstract;
using SimpraBitirme.DataAccessLayer.Concrete;
using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Dto.BasketDto;
using SimpraBitirme.EntityLayer.Dto.Products;

namespace SimpraBitirme.DataAccessLayer.EntityFramework
{
    public class EfBasketDal : Repository<Basket>, IBasketDal
    {

        private readonly DbContextOptions<Context> _options;
        private readonly IHttpContextAccessorService _httpContextAccessor;

        public EfBasketDal(DbContextOptions<Context> options, IHttpContextAccessorService httpContextAccessorService) : base(new Context(options), httpContextAccessorService)
        {
            _options = options;
            _httpContextAccessor = httpContextAccessorService;
        }

        public List<BasketResponse> GetAll()
        {
            using (var context = new Context(_options))
            {
                return (from cs in context.Baskets
                        orderby cs.Id ascending
                        select new BasketResponse
                        {
                            BasketItems = (from bi in cs.BasketItems
                                           join p in context.Products on bi.ProductId equals p.Id
                                           where p.Activity == true
                                           select new BasketItemResponse
                                           {
                                               ProductResponse = new ProductResponse
                                               {
                                                   Id = p.Id,
                                                   Name = p.Name,
                                                   Price = p.Price,
                                                   Description = p.Description,
                                                   Url = p.Url,
                                                   MaxPoint = p.MaxPoint,
                                                   PointEarningPercentage = p.PointEarningPercentage,
                                                   Quantity = p.Quantity,
                                                   Activity = p.Activity,
                                               },
                                               Id = bi.Id
                                           }).ToList(),
                            CouponCode = cs.CouponCode,
                            CouponPrice = cs.CouponPrice,
                            UserId = cs.UserId
                            
                        }).ToList();
            }
        }

        public BasketResponse GetById(int id)
        {
            using (var context = new Context(_options))
            {
                return (from cs in context.Baskets
                        where cs.Id == id
                        select new BasketResponse
                        {
                            BasketItems = (from bi in cs.BasketItems
                                           join p in context.Products on bi.ProductId equals p.Id
                                           where p.Activity == true
                                           select new BasketItemResponse
                                           {
                                               ProductResponse = new ProductResponse
                                               {
                                                   Id = p.Id,
                                                   Name = p.Name,
                                                   Price = p.Price,
                                                   Description = p.Description,
                                                   Url = p.Url,
                                                   MaxPoint = p.MaxPoint,
                                                   PointEarningPercentage = p.PointEarningPercentage,
                                                   Quantity = p.Quantity,
                                                   Activity = p.Activity,
                                               },
                                               Id = bi.Id,
                                           }).ToList(),
                            CouponCode = cs.CouponCode,
                            CouponPrice = cs.CouponPrice,
                            UserId = cs.UserId
                        }).FirstOrDefault();
            }
        }
        public BasketResponse GetByUserId(int userId)
        {
            using (var context = new Context(_options))
            {
                return (from cs in context.Baskets
                        where cs.UserId == userId
                        select new BasketResponse
                        {
                            BasketItems = (from bi in cs.BasketItems
                                           join p in context.Products on bi.ProductId equals p.Id
                                           where p.Activity == true
                                           select new BasketItemResponse
                                           {
                                               ProductResponse = new ProductResponse
                                               {
                                                   Id = p.Id,
                                                   Name = p.Name,
                                                   Price = p.Price,
                                                   Description = p.Description,
                                                   Url = p.Url,
                                                   MaxPoint = p.MaxPoint,
                                                   PointEarningPercentage = p.PointEarningPercentage,
                                                   Quantity = p.Quantity,
                                                   Activity = p.Activity,
                                               },
                                               Quantity = bi.Quantity,
                                               ProductId = bi.ProductId,
                                               BasketId = bi.BasketId,
                                               Id = bi.Id,
                                           }).ToList(),
                            CouponCode = cs.CouponCode,
                            CouponPrice = cs.CouponPrice,
                            UserId = cs.UserId,
                        }).FirstOrDefault();
                }
        }
    }
}
