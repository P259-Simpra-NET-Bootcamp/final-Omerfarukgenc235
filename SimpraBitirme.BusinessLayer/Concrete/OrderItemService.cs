using AutoMapper;
using SimpraBitirme.BusinessLayer.Abstract;
using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.DataAccessLayer.Abstract;
using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Dto.OrderItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.BusinessLayer.Concrete
{
    public class OrderItemService : IOrderItemService
    {
        IOrderItemDal _orderItemDal;
        private readonly IMapper _mapper;
        IHttpContextAccessorService _httpContextAccessorService;

        public OrderItemService(IOrderItemDal orderItemDal, IMapper mapper)
        {
            _orderItemDal = orderItemDal;
            _mapper = mapper;
        }

        public ApiResponse Add(OrderItemRequest orderItem)
        {
            var mapped = _mapper.Map<OrderItem>(orderItem);
            var response = _orderItemDal.Insert(mapped);
            if (response > 0)
                return new ApiResponse("İşlem başarılı bir şekilde gerçekleştirilmiştir.");
            return new ApiResponse("İşlem sırasında bir hata meydana geldiğinden dolayı ekleme işlemi gerçekleştirilememiştir..");
        }

        public ApiResponse<OrderItemResponse> GetByID(int id)
        {
            var response = _orderItemDal.Find(x => x.Id == id);
            var mapped = _mapper.Map<OrderItemResponse>(response);
            return new ApiResponse<OrderItemResponse>(mapped);    
        }

        public ApiResponse<List<OrderItemResponse>> GetList()
        {
            var response = _orderItemDal.List();
            var mapped = _mapper.Map<List<OrderItemResponse>>(response);
            return new ApiResponse<List<OrderItemResponse>>(mapped);
        }
    }
}
