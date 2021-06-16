using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context
{
    public class CrowdfundingDbContext : DbContext
    {
        public CrowdfundingDbContext(DbContextOptions<CrowdfundingDbContext> options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(e => e.HasIndex(c => c.Name).IsUnique());
        }
    }
}
