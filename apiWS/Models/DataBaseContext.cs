using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace apiWS.Models
{
    public class DataBaseContext : DbContext
    {

        public DataBaseContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Products> products { get; set; }
        public DbSet<Categories> categories { get; set; }
        public DbSet<Locations> locations { get; set; }

    }
}
