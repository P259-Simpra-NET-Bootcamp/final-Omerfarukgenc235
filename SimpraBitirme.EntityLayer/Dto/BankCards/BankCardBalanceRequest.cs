using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.EntityLayer.Dto.BankCards
{
    public class BankCardBalanceRequest
    {
        public string CardNumber { get; set; }
        public double Balance { get; set; }
    }
}
