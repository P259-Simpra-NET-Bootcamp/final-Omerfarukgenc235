using Microsoft.EntityFrameworkCore;
using SimpraBitirme.DataAccessLayer.Abstract;
using SimpraBitirme.DataAccessLayer.Concrete;
using SimpraBitirme.EntityLayer.Concrete;

namespace SimpraBitirme.DataAccessLayer.EntityFramework
{
    public class EfCategoryDal : Repository<Category>, ICategoryDal
    {
        private readonly DbContextOptions<Context> _options;
        private readonly IHttpContextAccessorService _httpContextAccessor;

        public EfCategoryDal(DbContextOptions<Context> options, IHttpContextAccessorService httpContextAccessorService) : base(new Context(options), httpContextAccessorService)
        {
            _options = options;
            _httpContextAccessor = httpContextAccessorService;
        }
    }
}
