using AutoMapper;
using SimpraBitirme.BusinessLayer.Abstract;
using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.DataAccessLayer.Abstract;
using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Dto.BasketDto;
using SimpraBitirme.EntityLayer.Dto.Categories;

namespace SimpraBitirme.BusinessLayer.Concrete
{
    public class BasketService : IBasketService
    {
        IBasketDal _basketDal;
        ICouponService _couponService;
        private readonly IMapper _mapper;
        IHttpContextAccessorService _httpContextAccessorService;

        public BasketService(IBasketDal basketDal, IMapper mapper, IHttpContextAccessorService httpContextAccessorService, ICouponService couponService)
        {
            _basketDal = basketDal;
            _mapper = mapper;
            _httpContextAccessorService = httpContextAccessorService;
            _couponService = couponService;
        }

        public ApiResponse Add(BasketInsertRequest basketInsertRequest)
        {
            var mapped = _mapper.Map<Basket>(basketInsertRequest);
            var response = _basketDal.Insert(mapped);
            if (response > 0)
                return new ApiResponse("İşlem başarılı bir şekilde gerçekleştirilmiştir.");
            return new ApiResponse("İşlem gerçekleştirilirken bir hata meydana gelmiştir.");
        }

  

        public BasketResponse GetByID(int id)
        {
            var response = _basketDal.GetById(id);
            return response;
        }

        public BasketResponse GetByUserId(int userId)
        {
            var response = _basketDal.GetByUserId(userId);
            return response;
        }

        public BasketResponse GetByAuthenticationUserBasket()
        {
            var userId = _httpContextAccessorService.GetUserId();
            var response = _basketDal.GetByUserId(userId);
            return response;
        }


        public List<BasketResponse> GetList()
        {
            var response = _basketDal.GetAll();
            return response;
        }

        public ApiResponse UseCouponCode(string couponCode)
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.Success = false;
            var userId = _httpContextAccessorService.GetUserId();

            var checkCoupon = _couponService.GetByCode(couponCode);

            var response = _basketDal.Find(x => x.UserId == userId);
            if (response == null)
            {
                apiResponse.Message = "Bu kullanıcıya ait bir sepet bulunamamıştır.";
                return apiResponse;
            }

            if (checkCoupon.UserId != userId)
            {
                apiResponse.Message = "Lütfen sadece size özel tanımlamanan kuponu kullanınız.";
                return apiResponse;
            }
            if (!checkCoupon.Status)
            {
                apiResponse.Message = "Bu kupon daha önceden kullanılmıştır.";
                return apiResponse;
            }
            if (checkCoupon.ExpireDate < DateTime.Now)
            {
                apiResponse.Message = "Kuponun son kullanma tarihi geçmiştir";
                return apiResponse;
            }
        
            response.CouponCode = couponCode;
            response.CouponPrice = checkCoupon.Price;

            var useCouponCode = _basketDal.Update(response);

            if (useCouponCode > 0)
            {
                apiResponse.Success = true;
                apiResponse.Message = "Kupon kodu başarılı bir şekilde uygulanmıştır.";
                return apiResponse;
            }
            apiResponse.Message = "İşlem sırasında bir hata meydana gelmiştir";
            return apiResponse;
        }
        public ApiResponse RemoveCouponCode()
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.Success = false;

            var userId = _httpContextAccessorService.GetUserId();

            var response = _basketDal.Find(x => x.UserId == userId);

            if (response == null) 
            {
                apiResponse.Message = "İşlem sırasında bir hata meydana gelmiştir.";
                return apiResponse;
            }

            if(response.CouponCode == null)
            {
                apiResponse.Message = "İşlem sırasında bir hata meydana gelmiştir.";
                return apiResponse;
            }

            response.CouponCode = null;
            response.CouponPrice = null;

            var updateResponse = _basketDal.Update(response);

            if(updateResponse > 0)
            {
                apiResponse.Success = true;
                apiResponse.Message = "Kupon başarılı bir şekilde kaldırılmıştır.";
                return apiResponse;
            }
            apiResponse.Message = "İşlem sırasında bir hata meydana gelmiştir.";
            return apiResponse;
        }
    }
}