using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.EntityLayer.Dto.BankCards;
using SimpraBitirme.EntityLayer.Dto.CategoryProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.BusinessLayer.Abstract
{
    public interface IBankCardService
    {
        ApiResponse FakePayment(BankCardFakePaymentRequest bankCardFakePaymentRequest);
        ApiResponse Add(BankCardRequest bankCardRequest);
        ApiResponse AddBalance(BankCardBalanceRequest bankCardBalanceRequest);
    }
}
