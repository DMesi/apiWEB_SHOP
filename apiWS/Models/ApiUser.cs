using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiWS.Models
{
    // prosirujemo za IdentityUser
    public class ApiUser : IdentityUser
   {
            public string FirstName { get; set; }
            public string LastName { get; set; }       
    }
}
