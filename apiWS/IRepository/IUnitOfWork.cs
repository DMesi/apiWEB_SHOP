using apiWS.Models;
using apiWS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiWS.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Categories> Categories { get; }
        IGenericRepository<Locations> Locations { get; }
        IGenericRepository<Products> Products { get; }
        Task Save();

    }
}
