using PersonServices.Model;
using Microsoft.EntityFrameworkCore;

namespace PersonServices.Context
{
    public class PgDbContext : DbContext
    {
        public PgDbContext(DbContextOptions<PgDbContext> options) : base(options)
        {

        }


        public DbSet<Person> Persons { get; set; }
        public DbSet<ContactInfo> Contacts { get; set; }

    }
}
