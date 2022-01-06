using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webWS.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int Id_categories { get; set; }
        //  public CategoriesDTO Categories { get; set; }
        public int Id_location { get; set; }
        //   public LocationsDTO Locations { get; set; }  // ovo znaci da cemo pristupiti svemu u tabeli lokacija
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public byte[] Picture { get; set; }

    }
}
