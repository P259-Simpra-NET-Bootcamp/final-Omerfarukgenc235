using AutoMapper;
using SimpraBitirme.BusinessLayer.Abstract;
using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.DataAccessLayer.Abstract;
using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Concrete.Dto.BasketDto;
using SimpraBitirme.EntityLayer.Dto.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.BusinessLayer.Concrete
{
    public class BasketItemService : IBasketItemService
    {
        IBasketDal _basketDal;
        IBasketItemDal _basketItemDal;
        IProductDal _productDal;
        IHttpContextAccessorService _httpContextAccessorService;
        private readonly IMapper _mapper;

        public BasketItemService(IBasketItemDal basketItemDal, IMapper mapper, IBasketDal basketDal, IHttpContextAccessorService httpContextAccessorService, IProductDal productDal)
        {
            _basketItemDal = basketItemDal;
            _mapper = mapper;
            _basketDal = basketDal;
            _httpContextAccessorService = httpContextAccessorService;
            _productDal = productDal;
        }

        public ApiResponse Add(BasketItemDto basketItem)
        {
            var apiResponse = new ApiResponse();
            apiResponse.Success = false;

            var checkProductQuantity = _productDal.Find(x => x.Id == basketItem.ProductId);
            
            if(checkProductQuantity.Quantity < basketItem.Quantity)
            {
                apiResponse.Message = $"Lütfen geçerli bir adet giriniz. En fazla {checkProductQuantity.Quantity} adet abilirisiniz.";
                return apiResponse;
            }

            if(!checkProductQuantity.Activity)
            {
                apiResponse.Message = "Bu ürün artık satılmıyor.";
                return apiResponse;
            }
                                    
            var userId = _httpContextAccessorService.GetUserId();
                                                       
            var basketId = _basketDal.Find(x => x.UserId == userId);

            var checkBasket = _basketItemDal.Find(x => x.ProductId == basketItem.ProductId && x.BasketId == basketId.Id);
            if (checkBasket != null)
            {
                apiResponse.Message = "Ürün sepette mevcut olduğundan dolayı eklenememiştir.";
                return apiResponse;
            }

            var response = _mapper.Map<BasketItem>(basketItem);
            response.BasketId = basketId.Id;
            int status = _basketItemDal.Insert(response);
            if(status > 0)
            {
                apiResponse.Message = "Ürün sepete başarılı bir şekilde eklenmiştir.";
                apiResponse.Success = true;
                return apiResponse;
            }

            return new ApiResponse("Ürün sepete eklenirken bir hata meydana gelmiştir.");
        }

        public ApiResponse Delete(int id)
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.Success = false;
            var userId = _httpContextAccessorService.GetUserId();
            var getById = _basketItemDal.Find(x => x.Id == id);
            if(getById.CreatedBy != userId)
            {
                apiResponse.Message = "İşlem sırasında bir hata meydana gelmiştir.";
                return apiResponse;
            }

            var response = _basketItemDal.Delete(getById);
            if (response > 0)
            {
                apiResponse.Success = true;
                apiResponse.Message = "Ürün sepetten başarılı bir şekilde çıkartılmıştır.";
            }
            apiResponse.Message = "Ürün sepetten çıkartılırken bir hata meydana gelmitşir.";
            return apiResponse;
        }

        public ApiResponse<List<BasketItem>> GetListByBasketId(int basketId)
        {
            var basketItemList = _basketItemDal.List().Where(x => x.BasketId == basketId).ToList();
            if (basketItemList.Count > 0)
                return new ApiResponse<List<BasketItem>>(basketItemList);
            return new ApiResponse<List<BasketItem>>("Sepet boş");

        }
    }
}
