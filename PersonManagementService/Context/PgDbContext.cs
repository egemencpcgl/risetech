using PersonServices.Model;
using Microsoft.EntityFrameworkCore;

namespace PersonServices.Context
{
    public class PgDbContext : DbContext
    {
        public PgDbContext()
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<ContactInfo> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=RISE;User ID=postgres;Password=123456;Integrated Security=true;Pooling=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
