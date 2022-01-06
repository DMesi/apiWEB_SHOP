using apiWS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace apiWS.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
    Task<IList<T>> GetAll(

     Expression<Func<T, bool>> expresson = null,
     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
     List<string> includes = null

     );

        Task<IPagedList<T>> GetPagedList(
            RequestParams requestParams,
            List<string> includes = null
            
            );

        Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null);
        Task Insert(T entity);
        Task Delete(int id);
        void Update(T entity);

        Task<IList<T>> GetAllByCategory(

    Expression<Func<T, bool>> expresson = null,
    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
    List<string> includes = null

    );


        List<joinTable> GetAllByCategoryMeni(

   Expression<Func<T, bool>> expresson = null
   

   );

    }

}
