using apiWS.IRepository;
using apiWS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiWS.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _context;

        private IGenericRepository<Categories> _categories;
        private IGenericRepository<Locations> _locations;
        private IGenericRepository<Products> _products;
        public UnitOfWork(DataBaseContext context)
        {
            _context = context;
        }

        public IGenericRepository<Categories> Categories => _categories ??= new GenericRepositroy<Categories>(_context);

        public IGenericRepository<Locations> Locations => _locations ??= new GenericRepositroy<Locations>(_context);

        public IGenericRepository<Products> Products => _products ??= new GenericRepositroy<Products>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);

        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
