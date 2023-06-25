using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.DataAccessLayer.Abstract
{
    public interface IRepository<T>
    {
        List<T> List();
        int Insert(T p);
        int InsertIdResponse(T p);
        int Update(T p);
        int Delete(T p);
        bool Any(Expression<Func<T, bool>> where);
        List<T> List(Expression<Func<T, bool>> filter);
        T Find(Expression<Func<T, bool>> where);
    }
}
