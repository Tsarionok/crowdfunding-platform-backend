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

        public DbSet<User> Users { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<UserProject> UserProjects { get; set; }
        
        public DbSet<Comment> Comments { get; set; }

        public DbSet<DonationHistory> DonationHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(e => e.HasIndex(c => c.Name).IsUnique());
        }
    }
}
