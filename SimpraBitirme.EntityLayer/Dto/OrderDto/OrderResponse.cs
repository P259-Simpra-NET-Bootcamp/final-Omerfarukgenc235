using SimpraBitirme.EntityLayer.Dto.BasketDto;
using SimpraBitirme.EntityLayer.Dto.OrderItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.EntityLayer.Dto.OrderDto
{
    public class OrderResponse
    {
        public int UserId { get; set; }
        public string OrderNumber { get; set; }
        public string? CouponCode { get; set; }
        public double? CouponPrice { get; set; }
        public double TotalPrice { get; set; }
        public double? PointPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemResponse> OrderItemResponse { get; set; }
    }
}
