using Microsoft.EntityFrameworkCore;

namespace DataTransfer.Models
{
    public class CountryContext : DbContext
    {
        public CountryContext(DbContextOptions<CountryContext> options): base(options) { }
        public DbSet<Country> countries { get; set; }
        public DbSet<Game> games { get; set; } = null;
        public DbSet<Sport> sports { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Game>().HasData(new Game { GameId = "winter", Name = "Winter Olympics" }) ;
            modelBuilder.Entity<Sport>().HasData(new Sport { SportId = "curling", Name = "Curling", Category = "Indoor" });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = "can", Name = "Canada", Game = new Game {GameId="all", Name="Winter Olympics" }, Sport = new Sport { SportId="all", Name="Curling", Category="Indoor"}, CountryFlag="canada.jpg" });
        }
    }
}
