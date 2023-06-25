using AutoMapper;
using SimpraBitirme.BusinessLayer.Abstract;
using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.DataAccessLayer.Abstract;
using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Dto.Categories;

namespace SimpraBitirme.BusinessLayer.Concrete
{
    public class CategoryService : ICategoryService
    {
        ICategoryDal _categoyDal;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryDal categoyDal, IMapper mapper)
        {
            _categoyDal = categoyDal;
            _mapper = mapper;
        }
        public virtual ApiResponse Add(CategoryRequest category)
        {

            ApiResponse apiResponse = new ApiResponse();
            apiResponse.Success = false;
            try
            {
                var anyName = _categoyDal.Find(x => x.Name == category.Name);

                if(anyName != null)
                {
                    apiResponse.Message = "Böyle bir kategori adı bulunmaktadır.";
                    return apiResponse;
                }

                var response = _mapper.Map<Category>(category);
                int status = _categoyDal.Insert(response);
                if(status > 0)
                {
                    apiResponse.Success = true;
                    apiResponse.Message = "Kategori başarılı bir şekilde eklenmiştir.";
                    return apiResponse;
                }
                apiResponse.Message = "Veri eklenirken bir hata meydana gelmiştir.";
                return apiResponse;
            }
            catch
            {
                apiResponse.Message = "Veri eklenirken bir hata meydana gelmiştir.";
                return apiResponse;
            }
        }

        public bool Delete(int id)
        {
            var item = _categoyDal.Find(x=> x.Id == id);
            item.IsDelete = true;
            int result = _categoyDal.Update(item);
            if (result > 0) return true;
            else return false;
        }

        public CategoryResponse GetByID(int id)
        {
            var category = _categoyDal.Find(x => x.Id == id);
            var mapped = _mapper.Map<CategoryResponse>(category);
            return mapped;
        }

        public List<CategoryResponse> GetList()
        {
            var categoryList = _categoyDal.List();
            var mapped = _mapper.Map<List<CategoryResponse>>(categoryList);
            return mapped;
        }

        public virtual ApiResponse Update(CategoryRequest category)
        {
            try
            {
                var anyName = _categoyDal.Find(x => x.Name == category.Name && x.Id != category.Id);

                if (anyName != null)
                {
                    return new ApiResponse("Böyle bir kategori adı bulunmaktadır.");
                }

                var response = _mapper.Map<Category>(category);
                int status = _categoyDal.Update(response);
                return new ApiResponse();
            }
            catch
            {
                return new ApiResponse("Veri güncellenirken bir hata meydana gelmiştir.");
            }
        }
    }
}
