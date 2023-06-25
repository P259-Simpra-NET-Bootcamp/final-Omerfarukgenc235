using SimpraBitirme.EntityLayer.Dto.BankCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.EntityLayer.Dto.OrderDto
{
    public class OrderRequest
    {
        public BankCardFakePaymentRequest bankCardFakePaymentRequest  { get; set; }
    }
}
