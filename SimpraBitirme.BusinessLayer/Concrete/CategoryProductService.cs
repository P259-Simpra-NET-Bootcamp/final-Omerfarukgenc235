﻿using AutoMapper;
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
            throw new NotImplementedException();
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
            var mapped = _mapper.Map<CategoryProduct>(categoryProduct);
            var response = _categoryProductDal.Insert(mapped);
            if (response > 0)
                return new ApiResponse("İşlem başarılı bir şekilde gerçekleştirilmiştir");
            return new ApiResponse("İşlem sırasında bir hata meydana gelmiştir!");

        }
    }
}