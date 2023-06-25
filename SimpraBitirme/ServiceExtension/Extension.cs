using AutoMapper;
using BusinessLayer.AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpraBitirme.BusinessLayer.Abstract;
using SimpraBitirme.BusinessLayer.Concrete;
using SimpraBitirme.DataAccessLayer.Abstract;
using SimpraBitirme.DataAccessLayer.Concrete;
using SimpraBitirme.DataAccessLayer.EntityFramework;

namespace SimpraOdev2.ServiceExtension
{
    public static class Extension
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            #region Veri Tabanı Bağlantısı
            services.AddDbContext<Context>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("SqlServerConnection")));
            #endregion
            services.AddHttpContextAccessor();
           
            services.AddScoped<IHttpContextAccessorService, HttpContextAccessorService>();
        
            #region AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            #endregion
         
            services.AddSingleton(config.CreateMapper());

            services.AddScoped<IHttpContextAccessorService, HttpContextAccessorService>();

            #region Product
            services.AddScoped<ICategoryDal, EfCategoryDal>();
            services.AddScoped<ICategoryService, CategoryService>();
            #endregion

            #region User
            services.AddScoped<IUserDal, EfUserDal>();
            services.AddScoped<IUserService, UserService>();
            #endregion

            #region Coupon
            services.AddScoped<ICouponDal, EfCouponDal>();
            services.AddScoped<ICouponService, CouponService>();
            #endregion

            #region Product
            services.AddScoped<IProductDal, EfProductDal>();
            services.AddScoped<IProductService, ProductService>();
            #endregion

            #region CategoryProduct
            services.AddScoped<ICategoryProductDal, EfCategoryProductDal>();
            services.AddScoped<ICategoryProductService, CategoryProductService>();
            #endregion

            #region Basket
            services.AddScoped<IBasketDal, EfBasketDal>();
            services.AddScoped<IBasketService, BasketService>();
            #endregion

            #region BasketItem
            services.AddScoped<IBasketItemDal, EfBasketItemDal>();
            services.AddScoped<IBasketItemService, BasketItemService>();
            #endregion

            #region Order
            services.AddScoped<IOrderDal, EfOrderDal>();
            services.AddScoped<IOrderService, OrderService>();
            #endregion

            #region OrderItem
            services.AddScoped<IOrderItemDal, EfOrderItemDal>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            #endregion

            #region BankCard
            services.AddScoped<IBankCardDal, EfBankCardDal>();
            services.AddScoped<IBankCardService, BankCardService>();
            #endregion
            


        }
    }
}

