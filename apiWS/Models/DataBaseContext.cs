using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiWS.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace apiWS.Models
{
    public class DataBaseContext : IdentityDbContext<ApiUser>
    {

        public DataBaseContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Products> products { get; set; }
        public DbSet<Categories> categories { get; set; }
        public DbSet<Locations> locations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
        }

    }



    }
