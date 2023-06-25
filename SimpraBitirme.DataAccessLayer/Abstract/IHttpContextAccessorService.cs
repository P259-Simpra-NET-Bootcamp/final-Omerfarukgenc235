using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.DataAccessLayer.Abstract
{
    public interface IHttpContextAccessorService
    {
        int GetUserId();

    }
}
