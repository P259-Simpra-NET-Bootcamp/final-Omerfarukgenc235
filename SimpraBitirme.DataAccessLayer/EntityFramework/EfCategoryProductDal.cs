using Microsoft.EntityFrameworkCore;
using SimpraBitirme.DataAccessLayer.Abstract;
using SimpraBitirme.DataAccessLayer.Concrete;
using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Dto.CategoryProduct;

namespace SimpraBitirme.DataAccessLayer.EntityFramework
{
    public class EfCategoryProductDal : Repository<CategoryProduct>, ICategoryProductDal
    {
        private readonly DbContextOptions<Context> _options;
        private readonly IHttpContextAccessorService _httpContextAccessor;

        public EfCategoryProductDal(DbContextOptions<Context> options, IHttpContextAccessorService httpContextAccessorService) : base(new Context(options), httpContextAccessorService)
        {
            _options = options;
            _httpContextAccessor = httpContextAccessorService;
        }

        public List<CategoryProductResponse> GetAll()
        {
            using (var context = new Context(_options))
            {
                return (from cs in context.CategoryProducts
                        join category in context.Categories on cs.CategoryId equals category.Id
                        join product in context.Products on cs.ProductId equals product.Id
                        orderby cs.Id ascending
                        select new CategoryProductResponse
                        {
                            Id = cs.Id,
                            Product = product,
                            ProductId = cs.ProductId,
                            Category = category,
                            CategoryId = cs.CategoryId,
                        }).ToList();
            }
        }
        public CategoryProductResponse GetById(int id)
        {
            using (var context = new Context(_options))
            {
                return (from cs in context.CategoryProducts
                        join category in context.Categories on cs.CategoryId equals category.Id
                        join product in context.Products on cs.ProductId equals product.Id
                        where cs.Id == id
                        orderby cs.Id ascending
                        select new CategoryProductResponse
                        {
                            Id = cs.Id,
                            Product = product,
                            ProductId = cs.ProductId,
                            Category = category,
                            CategoryId = cs.CategoryId,
                        }).FirstOrDefault();
            }
        }
    }
}