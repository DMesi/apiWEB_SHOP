using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webWS.Models.ViewModel
{
    public class IndexVM
    {
        public IEnumerable<Product> ProductList { get; set; }
        public IEnumerable<Location> LocationList { get; set; }
    }
}
