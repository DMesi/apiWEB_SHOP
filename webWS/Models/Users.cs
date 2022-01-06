using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webWS.Models
{
    public class Users
    {
        public string email { get; set; }
        public string password { get; set; }
        public string Roles { get; set; }  // korisnik moze da ima samo jednu rolu
        public string Token { get; set; }
    }
}
