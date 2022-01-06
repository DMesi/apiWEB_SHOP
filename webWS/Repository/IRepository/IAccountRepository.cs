using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webWS.Models;

namespace webWS.Repository.IRepository
{
    public interface IAccountRepository : IRepository<Users>
    {

        Task<Users> LoginAsync(string url, Users objecToCreate);

        Task<bool> RegisterAsync(string url, Users objecToCreate);
    }
}
