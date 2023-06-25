using Microsoft.EntityFrameworkCore;
using SimpraBitirme.DataAccessLayer.Abstract;
using SimpraBitirme.DataAccessLayer.Concrete;
using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Dto.BasketDto;
using SimpraBitirme.EntityLayer.Dto.OrderDto;
using SimpraBitirme.EntityLayer.Dto.OrderItems;
using SimpraBitirme.EntityLayer.Dto.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.DataAccessLayer.EntityFramework
{
    public class EfOrderDal : Repository<Order>, IOrderDal
    {
        private readonly DbContextOptions<Context> _options;
        private readonly IHttpContextAccessorService _httpContextAccessor;

        public EfOrderDal(DbContextOptions<Context> options, IHttpContextAccessorService httpContextAccessorService) : base(new Context(options), httpContextAccessorService)
        {
            _options = options;
            _httpContextAccessor = httpContextAccessorService;
        }

        public List<OrderResponse> GetAll()
        {
            using (var context = new Context(_options))
            {
                return (from cs in context.Orders
                        orderby cs.Id ascending
                        select new OrderResponse
                        {
                            OrderItemResponse = (from bi in cs.OrderItem
                                           select new OrderItemResponse
                                           {
                                              OrderId = bi.OrderId,
                                              ProductId = bi.ProductId,
                                              ProductName = bi.ProductName,
                                              ProductPrice = bi.ProductPrice,
                                              ProductQuantity = bi.ProductQuantity                                          
                                            }).ToList(),
                            CouponCode = cs.CouponCode,
                            CouponPrice = cs.CouponPrice,
                            CreatedAt = cs.CreatedAt,
                            PointPrice = cs.PointPrice,
                            TotalPrice = cs.TotalPrice,
                            UserId = cs.UserId
                        }).ToList();
            }
        }

        public OrderResponse GetById(int id)
        {
            using (var context = new Context(_options))
            {
                return (from cs in context.Orders
                        where cs.Id == id
                        select new OrderResponse
                        {
                            OrderItemResponse = (from bi in cs.OrderItem
                                                 select new OrderItemResponse
                                                 {
                                                     OrderId = bi.OrderId,
                                                     ProductId = bi.ProductId,
                                                     ProductName = bi.ProductName,
                                                     ProductPrice = bi.ProductPrice,
                                                     ProductQuantity = bi.ProductQuantity
                                                 }).ToList(),
                            CouponCode = cs.CouponCode,
                            CouponPrice = cs.CouponPrice,
                            CreatedAt = cs.CreatedAt,
                            PointPrice = cs.PointPrice,
                            TotalPrice = cs.TotalPrice,
                            UserId = cs.UserId
                        }).FirstOrDefault();
            }
        }

        public List<OrderResponse> GetByUserId(int userId)
        {
            using (var context = new Context(_options))
            {
                return (from cs in context.Orders
                        where cs.UserId == userId
                        orderby cs.Id ascending
                        select new OrderResponse
                        {
                            OrderItemResponse = (from bi in cs.OrderItem
                                                 select new OrderItemResponse
                                                 {
                                                     OrderId = bi.OrderId,
                                                     ProductId = bi.ProductId,
                                                     ProductName = bi.ProductName,
                                                     ProductPrice = bi.ProductPrice,
                                                     ProductQuantity = bi.ProductQuantity
                                                 }).ToList(),
                            CouponCode = cs.CouponCode,
                            CouponPrice = cs.CouponPrice,
                            CreatedAt = cs.CreatedAt,
                            PointPrice = cs.PointPrice,
                            TotalPrice = cs.TotalPrice,
                            UserId = cs.UserId
                        }).ToList();
            }
        }
    }
   
}
