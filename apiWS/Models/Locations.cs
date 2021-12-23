using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiWS.Models
{
    public class Locations
    {
        [Key]
        public int Id { get; set; }
        public string Name_location { get; set; }
        public string City { get; set; }
        public string City_short { get; set; }
        public int Postal_Code { get; set; }
        public string Street { get; set; }
        public int Street_Number { get; set; }
        [ForeignKey("Id_location")]
        public virtual IList<Products> Products { get; set; }  // jedna lokacija moze da ima vise proizvoda, OVO JE FK U PRODUCTS, ovo je {include}

    }
}
