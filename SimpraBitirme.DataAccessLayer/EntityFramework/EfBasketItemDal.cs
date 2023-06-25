using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SimpraBitirme.DataAccessLayer.Abstract;
using SimpraBitirme.DataAccessLayer.Concrete;
using SimpraBitirme.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.DataAccessLayer.EntityFramework
{
    public class EfBasketItemDal : Repository<BasketItem>, IBasketItemDal
    {
        private readonly DbContextOptions<Context> _options;
        private readonly IHttpContextAccessorService _httpContextAccessor;

        public EfBasketItemDal(DbContextOptions<Context> options, IHttpContextAccessorService httpContextAccessorService) : base(new Context(options), httpContextAccessorService)
        {
            _options = options;
            _httpContextAccessor = httpContextAccessorService;
        }
    }
}
