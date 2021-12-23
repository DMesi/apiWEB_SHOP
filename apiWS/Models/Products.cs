using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiWS.Models
{
    public class Products
    {
       
        public int Id { get; set; }

        public int Id_location { get; set; }  //FK


        [ForeignKey("Id_categories")]
        public Categories categories { get; set; }
        public int Id_categories { get; set; } //FK

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string Picture { get; set; }

    }

    

    
}
