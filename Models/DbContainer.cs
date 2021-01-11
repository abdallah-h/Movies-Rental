using System.Data.Entity;

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
        public DbSet<Genre> Genre { get; set; }
        public DbSet<MembershipType> MembershipType { get; set; }

    }
}