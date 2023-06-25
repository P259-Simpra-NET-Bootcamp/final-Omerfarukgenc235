using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.EntityLayer.Dto.BankCards
{
    public class BankCardRequest
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string Expiration { get; set; }
    }
}
