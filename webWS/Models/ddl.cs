using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webWS.Models
{
    public class ddl
    {
    }

    public class locationDDL
    {
        public IEnumerable<SelectListItem> locationList { get; set; }
        public Location location { get; set; }

        public static implicit operator locationDDL(Location v)
        {
            throw new NotImplementedException();
        }
    }


}
