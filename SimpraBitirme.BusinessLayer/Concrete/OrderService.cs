using AutoMapper;
using SimpraBitirme.BusinessLayer.Abstract;
using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.DataAccessLayer.Abstract;
using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Dto.OrderDto;
using SimpraBitirme.EntityLayer.Dto.User;
using System.Transactions;

namespace SimpraBitirme.BusinessLayer.Concrete
{
    public class OrderService : IOrderService
    {
        IOrderDal _orderDal;
        IOrderItemDal _orderItemDal;
        IBasketService _basketService;
        IBasketItemService _basketItemService;
        ICouponDal _couponDal;
        IProductService _productService;
        IBankCardService _bankCardService;
        IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessorService _httpContextAccessor;

        public OrderService(IOrderDal orderDal, IMapper mapper, IOrderItemDal orderItemDal,
            IBasketService basketService, ICouponDal couponDal, IBankCardService bankCardService,
            IHttpContextAccessorService httpContextAccessor, IUserService userService, IProductService productService, IBasketItemService basketItemService)
        {
            _orderDal = orderDal;
            _mapper = mapper;
            _orderItemDal = orderItemDal;
            _basketService = basketService;
            _couponDal = couponDal;
            _bankCardService = bankCardService;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _productService = productService;
            _basketItemService = basketItemService;
        }

        public ApiResponse Add(OrderRequest orderRequest)
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.Success = false;

            if (orderRequest == null)
            {
                apiResponse.Message = "İşlem sırasında bir hata meydana gelmiştir.";
                return apiResponse;
            }
            var userId = _httpContextAccessor.GetUserId();
            var basket = _basketService.GetByUserId(userId);

            if (basket.BasketItems.Count < 1)
            {
                apiResponse.Message = "Sepetiniz boş olduğundan dolayı sipariş gerçekleştirilemedi.";
                return apiResponse;
            }
            using (var transaction = new TransactionScope())
            {
                try
                {
                    var mappedOrder = _mapper.Map<Order>(orderRequest);
                    mappedOrder.UserId = userId;
                    mappedOrder.TotalPrice = basket.TotalPrice;

                    if (basket.CouponCode != null)
                    {
                        var deger = _couponDal.Find(x => x.Code == basket.CouponCode && x.UserId == mappedOrder.UserId && x.Status == true);
                        if (deger != null)
                        {
                            mappedOrder.TotalPrice = mappedOrder.TotalPrice - deger.Price;
                            deger.Status = false;
                            _couponDal.Update(deger);
                        }
                    }

                    orderRequest.bankCardFakePaymentRequest.Price = basket.TotalPrice;


                    var usePointRequest = new PointBalanceRequest();
                    usePointRequest.Id = userId;
                    usePointRequest.PointBalance = basket.TotalPrice;

                    var useUserPoint = _userService.UseUserPoint(usePointRequest);
                    if (useUserPoint.Success)
                    {
                        orderRequest.bankCardFakePaymentRequest.Price = useUserPoint.Response;
                    }

                    mappedOrder.PointPrice = basket.TotalPrice - orderRequest.bankCardFakePaymentRequest.Price;

                    var payment = _bankCardService.FakePayment(orderRequest.bankCardFakePaymentRequest);
                    if (!payment.Success)
                    {
                        return payment;
                    }
                    do
                    {
                        mappedOrder.OrderNumber = GenerateRandomCode();
                    } 
                    while (CheckIfOrderNumberExists(mappedOrder.OrderNumber));
                    var orderInsert = _orderDal.InsertIdResponse(mappedOrder);
                    double totalPoint = 0;
                    double coursePoint = 0;
                    if (orderInsert > 0)
                    {
                        basket.BasketItems.ForEach(x =>
                        {
                            var orderItem = new OrderItem
                            {
                                ProductId = x.ProductId,
                                OrderId = orderInsert,
                                ProductName = x.ProductResponse.Name,
                                ProductPrice = x.ProductResponse.Price,
                                ProductQuantity = x.Quantity,
                            };
                            _orderItemDal.Insert(orderItem);

                            coursePoint = x.ProductResponse.Price * ((x.ProductResponse.PointEarningPercentage) / 100);

                            if (coursePoint > x.ProductResponse.MaxPoint)
                            {
                                totalPoint = totalPoint + x.ProductResponse.MaxPoint;
                            }
                            else
                            {
                                totalPoint = totalPoint + coursePoint;
                            }
                            var product = _productService.GetByID(x.ProductId);
                            product.Quantity = product.Quantity - x.Quantity;

                            if (product.Quantity < 0)
                            {
                                apiResponse.Message = "İşlem sırasında bir hata meydana gelmiştir.";
                                transaction.Dispose();
                                throw new Exception("Bir hata meydana geldi");
                            }

                            if (product.Quantity == 0)
                            {
                                product.Activity = false;
                            }

                            var productUpdate = _productService.Update(product);
                            if (!productUpdate.Success)
                            {
                                transaction.Dispose();
                            }

                            var deleteBasketItem = _basketItemService.Delete(x.Id);
                            if (!deleteBasketItem.Success)
                            {
                                transaction.Dispose();
                                throw new Exception("BasketItem silinemedi.");
                            }

                        });
                    }
                    var updatePointRequest = new PointBalanceRequest();
                    updatePointRequest.Id = userId;

                    if (mappedOrder.CouponPrice == null)
                    {
                        mappedOrder.CouponPrice = 0;
                    }


                    double pointRate = ((basket.TotalPrice - (orderRequest.bankCardFakePaymentRequest.Price + (double)mappedOrder.CouponPrice)) / basket.TotalPrice);

                    updatePointRequest.PointBalance = totalPoint - totalPoint * pointRate;

                    var updatePoint = _userService.UpdateUserPoint(updatePointRequest);

                    if (!updatePoint.Success)
                    {
                        apiResponse.Message = "Bir hata meydana geldi.";
                        return apiResponse;
                    }

                    transaction.Complete();

                    apiResponse.Message = "Sipariş başarılı bir şekilde oluşturulmuştur.";
                    apiResponse.Success = true;
                    return apiResponse;
                }
                catch (Exception ex)
                {
                    transaction.Dispose();
                    apiResponse.Message = "İşlem sırasında bir hata meydana gelmiştir.";

                    return apiResponse;
                }
            }
        }
        private bool CheckIfOrderNumberExists(string orderNumber)
        {
            var response = _orderDal.Any(x => x.OrderNumber == orderNumber);
            return response;
        }
        private string GenerateRandomCode()
        {
            Random random = new Random();
            string characters = "0123456789";

            char[] code = new char[9];

            for (int i = 0; i < 9; i++)
            {
                code[i] = characters[random.Next(characters.Length)];
            }

            return new string(code);
        }
        public OrderResponse GetByID(int id)
        {
            var response = _orderDal.GetById(id);
            var mapped = _mapper.Map<OrderResponse>(response);
            return mapped;
        }

        public List<OrderResponse> GetList()
        {
            var response = _orderDal.GetAll();
            var mapped = _mapper.Map<List<OrderResponse>>(response);
            return mapped;
        }

        public List<OrderResponse> GetListByUserId()
        {
            var userId = _httpContextAccessor.GetUserId();
            var response = _orderDal.GetByUserId(userId);
            return response;
        }
    }
}