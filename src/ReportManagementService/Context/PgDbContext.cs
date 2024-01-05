using Microsoft.EntityFrameworkCore;
using ReportManagementService.Models;
using System;

namespace ReportManagementService.Context
{
    public class PgDbContext : DbContext
    {
        public PgDbContext(DbContextOptions<PgDbContext> options) : base(options)
        {

        }

        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportDetail> ReportDetail { get; set; }

    }
}
