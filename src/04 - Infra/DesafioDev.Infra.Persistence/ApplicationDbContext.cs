using DesafioDev.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DesafioDev.Infra.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Establishment> Establishments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Owner> Owners { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
