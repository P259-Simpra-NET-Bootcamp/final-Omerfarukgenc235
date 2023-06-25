using AutoMapper;
using SimpraBitirme.BusinessLayer.Abstract;
using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.DataAccessLayer.Abstract;
using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Dto.CategoryProduct;

namespace SimpraBitirme.BusinessLayer.Concrete
{
    public class CategoryProductService : ICategoryProductService
    {
        ICategoryProductDal _categoryProductDal;
        private readonly IMapper _mapper;

        public CategoryProductService(ICategoryProductDal categoryProductDal, IMapper mapper)
        {
            _categoryProductDal = categoryProductDal;
            _mapper = mapper;
        }

        public bool Delete(int id)
        {
            var response = _categoryProductDal.Find(x => x.Id == id);
         
            var deleteResponse = _categoryProductDal.Delete(response);

            if(deleteResponse > 0)
                return true;

            return false;
        }

        public CategoryProductResponse GetByID(int id)
        {
            var deger = _categoryProductDal.GetById(id);
            return deger;
        }

        public List<CategoryProductResponse> GetList()
        {
            var deger = _categoryProductDal.GetAll();
            return deger;
        }

        public ApiResponse Add(CategoryProductRequest categoryProduct)
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.Success = false;
            try
            {
                var check = _categoryProductDal.Any(x => x.ProductId == categoryProduct.ProductId && x.CategoryId == categoryProduct.CategoryId);

                if(check)
                {
                    apiResponse.Message = "Kategori zaten ekli.";
                    return apiResponse;
                }

                var mapped = _mapper.Map<CategoryProduct>(categoryProduct);
                var response = _categoryProductDal.Insert(mapped);
                if (response > 0)
                {
                    apiResponse.Message = "İşlem başarılı bir şekilde gerçekleştirilmiştir";
                    apiResponse.Success = true;
                    return apiResponse;
                }
                apiResponse.Message = "İşlem sırasında bir hata meydana gelmiştir!";
                return apiResponse;
            }
            catch
            {
                apiResponse.Message = "İşlem sırasında bir hata meydana gelmiştir!";
                return apiResponse;
            }
        }

        public List<CategoryProductResponse> GetListByCategoryId(int categoryId)
        {
            var deger = _categoryProductDal.GetAllByCategoryId(categoryId);
            return deger;
        }
    }
}