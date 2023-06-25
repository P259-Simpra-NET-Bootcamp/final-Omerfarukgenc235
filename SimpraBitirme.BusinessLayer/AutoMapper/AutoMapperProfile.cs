using AutoMapper;
using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Concrete.Dto.BasketDto;
using SimpraBitirme.EntityLayer.Dto.BankCards;
using SimpraBitirme.EntityLayer.Dto.Categories;
using SimpraBitirme.EntityLayer.Dto.CategoryProduct;
using SimpraBitirme.EntityLayer.Dto.Coupons;
using SimpraBitirme.EntityLayer.Dto.OrderDto;
using SimpraBitirme.EntityLayer.Dto.OrderItems;
using SimpraBitirme.EntityLayer.Dto.Products;
using SimpraBitirme.EntityLayer.Dto.User;
using SimpraBitirme.EntityLayer.OrderDto;

namespace BusinessLayer.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryResponse>();
            CreateMap<CategoryRequest, Category>();

            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();

            CreateMap<Basket, BasketDto>().ReverseMap();

            CreateMap<UserRequest, User>().ReverseMap();
            CreateMap<UserResponse, User>().ReverseMap();
            CreateMap<UserToken, User>().ReverseMap();

            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

            CreateMap<Coupon, CouponResponse>().ReverseMap();
            CreateMap<Coupon, CouponRequest>().ReverseMap();

            CreateMap<Product, ProductResponse>().ReverseMap();
            CreateMap<Product, ProductRequest>().ReverseMap();

            CreateMap<CategoryProduct, CategoryProductResponse>().ReverseMap();
            CreateMap<CategoryProduct, CategoryProductRequest>().ReverseMap();

            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<Order, OrderRequest>().ReverseMap();

            CreateMap<OrderItem, OrderItemResponse>().ReverseMap();
            CreateMap<OrderItem, OrderItemRequest>().ReverseMap();

            CreateMap<BankCardRequest, BankCard>();
            CreateMap<BankCardBalanceRequest, BankCard>();
            CreateMap<BankCardFakePaymentRequest, BankCard>();


        }
    }
}
