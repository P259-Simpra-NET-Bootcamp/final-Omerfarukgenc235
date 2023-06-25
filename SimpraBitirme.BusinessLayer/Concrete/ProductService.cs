using AutoMapper;
using SimpraBitirme.BusinessLayer.Abstract;
using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.DataAccessLayer.Abstract;
using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Dto.Products;

namespace SimpraBitirme.BusinessLayer.Concrete
{
    public class ProductService : IProductService
    {
        IProductDal _productDal;
        private readonly IMapper _mapper;

        public ProductService(IProductDal productDal, IMapper mapper)
        {
            _productDal = productDal;
            _mapper = mapper;
        }

        public ApiResponse Add(ProductRequest product)
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.Success = false;
            try
            {
               
                var mapped = _mapper.Map<Product>(product);
                var response = _productDal.Insert(mapped);
                if (response > 0)
                {
                    apiResponse.Message = "İşlem başarılı bir şekilde gerçekleştirilmiştir.";
                    apiResponse.Success = true;
                    return apiResponse;
                }
                apiResponse.Message = "İşlem gerçekleşirken bir hata meydana gelmiştir!";
                return apiResponse;
            }
            catch
            {
                apiResponse.Message = "İşlem gerçekleşirken bir hata meydana gelmiştir!";
                return apiResponse;
            }
        }

        public bool Delete(int id)
        {
            var response = _productDal.Find(x => x.Id == id);
            response.IsDelete = true;
            var deleteResponse = _productDal.Delete(response);
            if (deleteResponse > 0)
                return true;
            return false;
        }

        public ProductResponse GetByID(int id)
        {
            var response = _productDal.Find(x => x.Id == id);
            var mapped = _mapper.Map<ProductResponse>(response);
            return mapped;
        }

        public List<ProductResponse> GetList()
        {
            var response = _productDal.List();
            var mapped = _mapper.Map<List<ProductResponse>>(response);
            return mapped;
        }

        public ApiResponse Update(ProductResponse product)
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.Success = false;
            try
            {
               
                var response = _productDal.Find(x => x.Id == product.Id);
                if (response != null)
                {
                    response.Description = product.Description;
                    response.Name = product.Name;
                    response.Property = product.Property;
                    response.Url = product.Url;
                    response.Tag = product.Tag;
                    response.Activity = product.Activity;
                    response.Quantity = product.Quantity;
                    response.Price = product.Price;
                    response.PointEarningPercentage = product.PointEarningPercentage;
                    response.MaxPoint = product.MaxPoint;
                    var update = _productDal.Update(response);
                    if (update > 0)
                    {
                        apiResponse.Success = true;
                        apiResponse.Message = "İşlem başarılı bir şekilde gerçekleştirilmiştir.";
                        return apiResponse;
                    }
                    apiResponse.Message = "İşlem gerçekleşirken bir hata meydana gelmiştir!";
                    return apiResponse;
                }
                apiResponse.Message = "Lütfen geçerli bir ürün seçiniz!";
                return apiResponse;
            }
            catch
            {
                apiResponse.Message = "İşlem gerçekleşirken bir hata meydana gelmiştir!";
                return apiResponse;
            }
        }
        public List<ProductResponse> GetFilterByName(string name)
        {
            var response = _productDal.GetFilterByName(name);
            return response;
        }

        public List<ProductResponse> GetFilterByPrice(ProductFilterRequest productFilterRequest)
        {
            var response = _productDal.GetFilterByPrice(productFilterRequest);
            return response;
        }
    }
}