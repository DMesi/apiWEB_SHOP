using apiWS.IRepository;
using apiWS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace apiWS.Repository
{
    public class GenericRepositroy<T> : IGenericRepository<T> where T : class
    {
        private readonly DataBaseContext _context;
        private readonly DbSet<T> _db;
        public GenericRepositroy(DataBaseContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }

        public async Task Delete(int id)
        {
            var entity = await _db.FindAsync(id);
            _db.Remove(entity);

        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
           
            IQueryable<T> query = _db;
            
            if (includes != null)
            {
                foreach (var includePropery in includes)
                {
                    query = query.Include(includePropery);
                }
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expresson = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _db;

            if (expresson != null)
            {
                query = query.Where(expresson);

            }
            if (includes != null)
            {
                foreach (var includePropery in includes)
                {
                    query = query.Include(includePropery);
                }
            }
            if (orderBy != null)
            {

                query = orderBy(query);
            }
            return await query.AsNoTracking().ToListAsync();

        }

        public async Task<IList<T>> GetAllByCategory(Expression<Func<T, bool>> expresson = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _db;

            if (expresson != null)
            {
                query = query.Where(expresson);

            }
            if (includes != null)
            {
                foreach (var includePropery in includes)
                {
                    query = query.Include(includePropery);
                }
            }
            if (orderBy != null)
            {

                query = orderBy(query);
            }
            return await query.AsNoTracking().ToListAsync();
        }


        public List<joinTable> GetAllByCategoryMeni(Expression<Func<T, bool>> expresson = null)
        {

         //"select count(proizvod.id_kategorije)as broj, kategorija.naziv as kn, kategorija.id_kategorije as ik
         //from proizvod join kategorija on proizvod.id_kategorije = kategorija.id_kategorije
         //group by kategorija.naziv,kategorija.id_kategorije"


            var query = _context.categories
                         .Join(
                         _context.products
                         , c => c.Id, p => p.Id_categories, (c, p) => new { Category5 = c, Product5 = p })
                         
                         .GroupBy(x => new { x.Category5.Name, x.Category5.Id})
                         
                         .Select(

                x => new joinTable
                {
                    ID = x.Key.Id,
                    Name = x.Key.Name,
                    Count =x.Count()
                }

                ).ToList();

            return query;
        }

        public async Task<IPagedList<T>> GetPagedList(RequestParams requestParams, List<string> includes = null)
        {
            IQueryable<T> query = _db;

         
            if (includes != null)
            {
                foreach (var includePropery in includes)
                {
                    query = query.Include(includePropery);
                }
            }
           
            return await query.AsNoTracking().ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }

        public async Task Insert(T entity)
        {
            await _db.AddAsync(entity);
        }

        //public string prikaziKategorije()
        //{
        //    var ukupnoProizvoda = _context.products.Select(x => x.Id).ToList().Count();

        //    var brojiKategorije = (from n in _context.products
        //                           join c in _context.categories on n.Id_categories equals c.Id

        //                           group new { n, c } by new { n.Id_categories, c.Id, c.Name }
        //                           into grp

        //                           select new joinTable()
        //                           {
        //                               broj =grp.Key.Id_categories,
        //                               kn =grp.Key.Name,
        //                               ik = grp.Key.Id

        //                           }).ToList();


        //    StringBuilder sb = new StringBuilder();

        //    int brojac = 1;

        //    sb.Append("<input type ='radio' id = 'odabir' name='odabir' value='0' checked> Sve (" + ukupnoProizvoda + ") <br/>");

        //    foreach (var pro in brojiKategorije)
        //    {
        //        sb.Append("<input type ='radio' id = 'odabir' name='odabir' value=" + brojac + "> " + pro.kn + " (" + pro.broj + ")  <br/>");

        //        brojac++;

        //    }

        //    return sb.ToString();

        //}

        public void Update(T entity)
        {
            _db.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

        }

       
    }


}
