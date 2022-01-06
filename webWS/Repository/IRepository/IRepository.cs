using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webWS.Repository
{
    public interface IRepository<T>where T :class
    {
        Task<T> GetAsync(string url, int Id, string token);
        Task<IEnumerable<T>> GetAllAsync(string url, string token);
        Task<IEnumerable<T>> GetAllByCategoryAsync(string url, int category);
        Task<bool> CreateAsync(string url, T objectToCreate, string token);
        Task<bool> UpdateAsync(string url, T objectToUpdate, string token);
        Task<bool> DeleteAsync(string url, int Id, string token);
        Task<IEnumerable<T>> CategoryMeni(string url);
    }
}
