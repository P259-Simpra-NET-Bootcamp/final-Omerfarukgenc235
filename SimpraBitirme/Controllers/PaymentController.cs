using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpraBitirme.BusinessLayer.Abstract;
using SimpraBitirme.BusinessLayer.Concrete;
using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.EntityLayer.Dto.BankCards;
using SimpraBitirme.EntityLayer.Dto.OrderDto;

namespace SimpraBitirme.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IBankCardService _bankCardService;

        public PaymentController(IBankCardService bankCardService)
        {
            _bankCardService = bankCardService;
        }

        [Authorize(Roles = "A")]
        [HttpPost]
        public ApiResponse Post([FromBody] BankCardRequest bankCardRequest)
        {
            var result = _bankCardService.Add(bankCardRequest);
            return result;
        }
        [Authorize(Roles = "A")]
        [HttpPut("AddBalance")]
        public ApiResponse Post([FromBody] BankCardBalanceRequest bankCardBalanceRequest)
        {
            var result = _bankCardService.AddBalance(bankCardBalanceRequest);
            return result;
        }

        [HttpPost("FakePayment")]
        public ApiResponse Post([FromBody] BankCardFakePaymentRequest bankCardFakePaymentRequest)
        {
            var result = _bankCardService.FakePayment(bankCardFakePaymentRequest);
            return result;
        }

    }
}
