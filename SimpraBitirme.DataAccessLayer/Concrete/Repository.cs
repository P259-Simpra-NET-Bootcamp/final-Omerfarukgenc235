using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SimpraBitirme.DataAccessLayer.Abstract;
using SimpraBitirme.EntityLayer.Concrete.Base;
using System.Linq.Expressions;
using System.Security.Claims;

namespace SimpraBitirme.DataAccessLayer.Concrete
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _object;
        private readonly IHttpContextAccessorService _httpContextAccessor;

        public Repository(DbContext dbContext, IHttpContextAccessorService httpContextAccessorService)
        {
            _dbContext = dbContext;
            _object = _dbContext.Set<T>();
            _httpContextAccessor = httpContextAccessorService;
        }

        public int Delete(T p)
        {
            _object.Remove(p);
            return _dbContext.SaveChanges();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return _object.FirstOrDefault(where);
        }
        public bool Any(Expression<Func<T, bool>> where)
        {
            return _object.Any(where);
        }
        public T GetByID(int id)
        {
            return _object.Find(id);
        }
    
        public int Insert(T p)
        {
            p.CreatedAt = DateTime.UtcNow;
            p.CreatedBy = _httpContextAccessor.GetUserId();
            _object.Add(p);
            return _dbContext.SaveChanges();
        }
        public int InsertIdResponse(T p)
        {
            p.CreatedAt = DateTime.UtcNow;
            p.CreatedBy = _httpContextAccessor.GetUserId();
            _object.Add(p);
            _dbContext.SaveChanges();
            return p.Id;
        }
        public List<T> List()
        {
            return _object.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _object.Where(where).ToList();
        }

        public int Update(T p)
        {
            var updatedEntity = _dbContext.Entry(p);
            updatedEntity.State = EntityState.Modified;
            return _dbContext.SaveChanges();
        }
    }
}