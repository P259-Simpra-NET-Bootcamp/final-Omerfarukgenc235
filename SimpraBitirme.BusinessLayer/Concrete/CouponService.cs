using AutoMapper;
using SimpraBitirme.BusinessLayer.Abstract;
using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.DataAccessLayer.Abstract;
using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Dto.Coupons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.BusinessLayer.Concrete
{
    public class CouponService : ICouponService
    {
        ICouponDal _couponDal;
        private readonly IMapper _mapper;
        IHttpContextAccessorService _httpContextAccessorService;

        public CouponService(ICouponDal couponDal, IMapper mapper)
        {
            _couponDal = couponDal;
            _mapper = mapper;
        }
        private string GenerateRandomCode()
        {
            Random random = new Random();
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            char[] code = new char[10];

            for (int i = 0; i < 10; i++)
            {
                code[i] = characters[random.Next(characters.Length)];
            }

            return new string(code);
        }
        public ApiResponse Add(CouponRequest coupon)
        {
            var mapped = _mapper.Map<Coupon>(coupon);
            mapped.Status = true;
            mapped.Code = GenerateRandomCode();

            var checkCouponCode = _couponDal.Any(x => x.Code == mapped.Code);
            if(checkCouponCode) 
            {
                return new ApiResponse("Böyle kupon zaten mevcut");
            }

            var response = _couponDal.Insert(mapped);

            if (response > 0)
            {
                return new ApiResponse("Başarılı Bir Şekilde Kupon Tanımlanmıştır.");
            }

            return new ApiResponse("Bir hata meydana gelmiştir");
        }

        public bool Delete(int id)
        {
            var response = _couponDal.Find(x => x.Id == id);
            if(response == null)
            {
                return false;
            }
            response.IsDelete = true;
            var deleteResponse = _couponDal.Update(response);
            if (deleteResponse > 0)
            {
                return true;

            }
            return false;
        }

        public CouponResponse GetByID(int id)
        {
            var coupon = _couponDal.Find(x => x.Id == id);
            var mapped = _mapper.Map<CouponResponse>(coupon);
            return mapped;
        }

        public List<CouponResponse> GetList()
        {
            var coupon = _couponDal.List();
            var mapped = _mapper.Map<List<CouponResponse>>(coupon);
            return mapped;
        }

        public CouponResponse GetByCode(string Code)
        {
            var coupon = _couponDal.Find(x => x.Code == Code);
            var mapped = _mapper.Map<CouponResponse>(coupon);
            return mapped;
        }

        public List<CouponResponse> GetByUserId(int userId)
        {
            var coupon = _couponDal.Find(x => x.UserId == userId);
            var mapped = _mapper.Map<List<CouponResponse>>(coupon);
            return mapped;
        }
    }
}