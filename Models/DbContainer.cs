using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Movies_Rental.Models
{
    public class DbContainer : DbContext
    {
        public DbContainer() : base("MoviesRental")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DbContainer, Migrations.Configuration>());
        }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Customer> Customer { get; set; }

    }
}