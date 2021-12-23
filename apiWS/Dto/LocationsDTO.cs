using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace apiWS.Models
{
    public class LocationsDTO
    {
        public int Id { get; set; }
        public string Name_location { get; set; }
        public string City { get; set; }
        public string City_short { get; set; }
        public int Postal_Code { get; set; }
        public string Street { get; set; }
        public int Street_Number { get; set; }
        public IList<ProductsDTO> Products { get; set; }  // jedna lokacija moze da ima vise proizvoda

    }

    public class LocationsCreateDTO
    {
      //  public int Id { get; set; }
        public string Name_location { get; set; }
        public string City { get; set; }
        public string City_short { get; set; }
        public int Postal_Code { get; set; }
        public string Street { get; set; }
        public int Street_Number { get; set; }
       
    }
}
