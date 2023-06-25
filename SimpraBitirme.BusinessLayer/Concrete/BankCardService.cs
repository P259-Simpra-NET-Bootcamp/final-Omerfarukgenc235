using AutoMapper;
using SimpraBitirme.BusinessLayer.Abstract;
using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.DataAccessLayer.Abstract;
using SimpraBitirme.DataAccessLayer.Concrete;
using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Dto.BankCards;
using SimpraBitirme.EntityLayer.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.BusinessLayer.Concrete
{
    public class BankCardService : IBankCardService
    {
        IBankCardDal _bankCardDal;
        IHttpContextAccessorService _httpContextAccessorService;
        private readonly IMapper _mapper;

        public BankCardService(IBankCardDal bankCardDal, IMapper mapper, IHttpContextAccessorService httpContextAccessorService)
        {
            _bankCardDal = bankCardDal;
            _mapper = mapper;
            _httpContextAccessorService = httpContextAccessorService;
        }
        private static string HashCardNumber(string cardNumber)
        {
            byte[] cardBytes = Encoding.UTF8.GetBytes(cardNumber);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(cardBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
        public ApiResponse Add(BankCardRequest bankCardRequest)
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.Success = false;
            try
            {            
                bankCardRequest.CardNumber = HashCardNumber(bankCardRequest.CardNumber);


                if (bankCardRequest.CVV.Length != 3)
                {
                    apiResponse.Message = "CVV 3 haneden oluşmak zorundadır.";
                    return apiResponse;
                }

                if (bankCardRequest.CVV.Length != 16)
                {
                    apiResponse.Message = "Banka kartı 16 haneden oluşmak zorundadır.";
                    return apiResponse;
                }


                bool checkBankCard = _bankCardDal.Any(x => x.CardNumber == bankCardRequest.CardNumber);
                if (checkBankCard)
                {
                    apiResponse.Message = "Böyle bir banka kartı zaten mevcut.";
                    return apiResponse;
                }
                var mapped = _mapper.Map<BankCard>(bankCardRequest);
                mapped.CreatedBy = _httpContextAccessorService.GetUserId();
                var response = _bankCardDal.Insert(mapped);
                if (response > 0)
                {
                    apiResponse.Success = true;
                    apiResponse.Message = "Kart başarılı bir şekilde eklenmiştir.";
                    return apiResponse;
                }
                apiResponse.Message = "İşlem sırasında bir hata meydana gelmiştir.";
                return apiResponse;
            }
            catch
            {
                apiResponse.Message = "İşlem sırasında bir hata meydana gelmiştir.";
                apiResponse.Success = false;
                return apiResponse;
            }

        public ApiResponse AddBalance(BankCardBalanceRequest bankCardBalanceRequest)
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.Success = false;

            if (bankCardBalanceRequest.Balance < 0)
            {
                apiResponse.Message = "İşlem sırasında bir hata meydana gelmiştir.";
                return apiResponse;
            }

            bankCardBalanceRequest.CardNumber = HashCardNumber(bankCardBalanceRequest.CardNumber);
            bool checkBankCard = _bankCardDal.Any(x => x.CardNumber == bankCardBalanceRequest.CardNumber);
            if (!checkBankCard)
            {
                apiResponse.Message = "Böyle bir banka kartı bulunamamıştır.";
                return apiResponse;
            }
            var getBankCard = _bankCardDal.Find(x => x.CardNumber == bankCardBalanceRequest.CardNumber);
            getBankCard.Balance = getBankCard.Balance + bankCardBalanceRequest.Balance;
            var response = _bankCardDal.Update(getBankCard);
            if (response > 0)
            {
                apiResponse.Message = "Bakiye başarılı bir şekilde eklenmiştir.";
                return apiResponse;
            }
            apiResponse.Message = "İşlem sırasında bir hata meydana gelmiştir.";
            return apiResponse;
        }

       

        public ApiResponse FakePayment(BankCardFakePaymentRequest bankCardFakePaymentRequest)
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.Success = false;
            bankCardFakePaymentRequest.CardNumber = HashCardNumber(bankCardFakePaymentRequest.CardNumber);

            if(bankCardFakePaymentRequest.Price < 0)
            {
                apiResponse.Message = "İşlem sırasında bir hata meydana gelmiştir.";
                return apiResponse;
            }

            bool checkBankCard = _bankCardDal.Any(x => x.CardNumber == bankCardFakePaymentRequest.CardNumber);
            if (!checkBankCard)
            {
                apiResponse.Message = "Böyle bir banka kartı bulunamamıştır.";
                return apiResponse;
            }


            var response = _bankCardDal.Find(x => x.CardNumber == bankCardFakePaymentRequest.CardNumber
                                    && x.CardName == bankCardFakePaymentRequest.CardName
                                    && x.CVV == bankCardFakePaymentRequest.CVV
                                    && x.Expiration == bankCardFakePaymentRequest.Expiration);
            if (response == null)
            {
                apiResponse.Message = "Girmiş olduğunuz kart bilgileri doğru değildir. Lütfen geçerli bir banka kartı giriniz.";

                return apiResponse;
            }

            if (response.Balance < bankCardFakePaymentRequest.Price)
            {
                apiResponse.Message = "Bakiye yetersiz olduğundan dolayı ödeme işlemi gerçekleştirilememiştir.";
                return apiResponse;
            }

            response.Balance = response.Balance - bankCardFakePaymentRequest.Price;

            var updateResponse = _bankCardDal.Update(response);
            
            if(updateResponse > 0)
            {
                apiResponse.Message = "Ödeme işlemi başarılı bir şekilde gerçekleştirilmiştir.";
                apiResponse.Success = true;
                return apiResponse;
            }

            apiResponse.Message = "Ödeme işlemi sırasında bir hata meydana gelmiştir.";

            return apiResponse;
        }

    
    }
}
