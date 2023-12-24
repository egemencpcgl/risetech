using Microsoft.EntityFrameworkCore;
using ReportManagementService.Models;
using System;

namespace ReportManagementService.Context
{
    public class PgDbContext : DbContext
    {
        public PgDbContext()
        {
        }

        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportDetail> ReportDetail { get; set; }

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
